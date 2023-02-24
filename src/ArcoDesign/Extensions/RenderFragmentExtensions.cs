using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.RenderTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Web;

namespace ArcoDesign.Extensions;
internal static class RenderFragmentExtensions {

    public static bool IsString(this RenderFragment self) {
        if(self is null)
            return false;
        var builder = new RenderTreeBuilder();
        self.Invoke(builder);

        var frames = builder.GetFrames().Array;
        var frame = frames.FirstOrDefault();
        var text = string.Empty;
        if (frame.FrameType == RenderTreeFrameType.Text) {
            text = frame.TextContent;
        }
        else if(frame.FrameType == RenderTreeFrameType.Markup) {
            text = frame.MarkupContent;
        }
        else {
            return false;
        }
        if(HttpUtility.HtmlEncode(text) == text)
            return false;
        return true;
    }

    public static string GetText(this RenderFragment self) {
        if (self is null)
            return string.Empty;
        var builder = new RenderTreeBuilder();
        self.Invoke(builder);

        var frames = builder.GetFrames().Array;
        var frame = frames.FirstOrDefault();
        if (frame.FrameType == RenderTreeFrameType.Text)
            return frame.TextContent;
        if(frame.FrameType == RenderTreeFrameType.Markup)
            return frame.MarkupContent;

        return string.Empty;
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

    public static bool IsNullOrEmpty(this RenderFragment self) {
        if (self is null)
            return true;

        var builder = new RenderTreeBuilder();
        self.Invoke(builder);
        var frames = builder.GetFrames().Array;
        
        var frame = frames.FirstOrDefault();
        if (frames.Any())
            return frames.First().FrameType == RenderTreeFrameType.None;
        return false;
    }

}
