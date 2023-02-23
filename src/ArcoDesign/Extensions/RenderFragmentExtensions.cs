using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.RenderTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ArcoDesign.Extensions;
internal static class RenderFragmentExtensions {

    public static bool IsString(this RenderFragment self) {
        if(self is null)
            return false;
        var builder = new RenderTreeBuilder();
        self.Invoke(builder);

        var frames = builder.GetFrames().Array;
        var frame = frames.FirstOrDefault();
        if (frame.FrameType == RenderTreeFrameType.Text)
            return true;
        return true;
    }

    public static bool IsComponent<T>(this RenderFragment self) 
        where T: ArcoDesignComponentBase {
        if (self is null)
            return false;

        var builder = new RenderTreeBuilder();
        self.Invoke(builder);
        var frames = builder.GetFrames().Array;
        var frame = frames.FirstOrDefault();
        if(frame.FrameType == RenderTreeFrameType.Component) {
            return frame.ComponentType.IsAssignableFrom(typeof(T));
        }

        return false;
    }

    public static bool IsHtml<T>(this RenderFragment self) {
        if (self is null)
            return false;
        var builder = new RenderTreeBuilder();
        self.Invoke(builder);

        var frames = builder.GetFrames().Array;
        return frames.Any(t => t.FrameType == RenderTreeFrameType.Markup);
    }

}
