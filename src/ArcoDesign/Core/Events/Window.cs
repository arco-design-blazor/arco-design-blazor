using ArcoDesign.Core;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace ArcoDesign.Core;
internal class Window {
    
}

//public class MediaQueryList {
//    private readonly IJSRuntime _jsRuntime;
//    private readonly DotNetObjectReference<MediaQueryList> _dotNetRef;

//    public bool Matches { get; private set; }
//    public string Media { get; }
//    public event EventHandler<MediaQueryListEventArgs>? OnChange;

//    public MediaQueryList(IJSRuntime jsRuntime, string media) {
//        _jsRuntime = jsRuntime;
//        Media = media;
//        _dotNetRef = DotNetObjectReference.Create(this);
//    }


//    [JSInvokable("MediaQueryListOnChange")]
//    public void HandleOnChange(bool matches) {
//        Matches = matches;
//        OnChange?.Invoke(this, new MediaQueryListEventArgs(matches, Media));
//    }

//    public async Task<bool> AddListener() {
//        var result = await _jsRuntime.InvokeAsync<object>("mediaQueryList.addListener", Media, _dotNetRef);
//        return Convert.ToBoolean(result);
//    }

//    public async Task<bool> RemoveListener() {
//        var result = await _jsRuntime.InvokeAsync<object>("mediaQueryList.removeListener", Media, _dotNetRef);
//        return Convert.ToBoolean(result);
//    }

//    public void Dispose() {
//        _dotNetRef?.Dispose();
//    }
//}

//public class MediaQueryListEventArgs : EventArgs {
//    public bool Matches { get; }
//    public string Media { get; }

//    public MediaQueryListEventArgs(bool matches, string media) {
//        Matches = matches;
//        Media = media;
//    }
//}