using Microsoft.JSInterop;
using System.Text.Json.Serialization;

using ScreenMap = System.Collections.Generic.Dictionary<ArcoDesign.Core.Breakpoint, bool>;
using BreakpointMap = System.Collections.Generic.Dictionary<ArcoDesign.Core.Breakpoint, string>;
namespace ArcoDesign.Core;

delegate void SubscribeDelegate(ScreenMap screens, Breakpoint? breakpointChecked);

public enum Breakpoint {
    [JsonPropertyName("xxxl")]
    XXXL,
    [JsonPropertyName("xxl")]
    XXL,
    [JsonPropertyName("xl")]
    XL,
    [JsonPropertyName("lg")]
    LG,
    [JsonPropertyName("md")]
    MD,
    [JsonPropertyName("sm")]
    SM,
    [JsonPropertyName("xs")]
    XS,
}

internal class ReponsiveObserver {

    private readonly IDictionary<string, MediaQueryMap> matchHandlers = new Dictionary<string, MediaQueryMap>();
    private readonly List<KeyValuePair<string, SubscribeDelegate>> _subscribers = new List<KeyValuePair<string, SubscribeDelegate>>();
    private ScreenMap screens = new ScreenMap();
    private readonly IJSRuntime jSRuntime;
    private int subUid = -1;

    private readonly BreakpointMap responsiveMap = new BreakpointMap{
        { Breakpoint.XS, "(max-width: 575px)" },
        { Breakpoint.SM, "(max-width: 576px)" },
        { Breakpoint.MD, "(max-width: 768px)" },
        { Breakpoint.LG, "(max-width: 992px)" },
        { Breakpoint.XL, "(max-width: 1200px)" },
        { Breakpoint.XXL, "(max-width: 1600px)" },
        { Breakpoint.XXXL, "(max-width: 2000px)" },
    };

    public ReponsiveObserver(IJSRuntime jSRuntime) {
        this.jSRuntime = jSRuntime;
    }

    public async Task Unregister() {
       foreach(var item in responsiveMap) {
            var matchMediaQuery = item.Value;
            var handler = this.matchHandlers[matchMediaQuery];
            if(handler != null) {
                await RemoveListener(handler.MediaQuery.Media, handler.Listener);
            }
        }
    }

    public async Task Register() {
        foreach (var (screen, matchMediaQuery) in responsiveMap) {
            Action<bool> listener = (matches) => {
                screens[screen] = matches;
                _ = this.Dispatch(screens, screen);
            };
            var mql = await jSRuntime.InvokeAsync<MediaQueryList>(matchMediaQuery);
            await AddListener(mql.Media, listener);
            this.matchHandlers.Add(matchMediaQuery, new MediaQueryMap { MediaQuery= mql, Listener = listener });
            listener.Invoke(mql.Matches);
        }
    }

    public async Task Subscribe(SubscribeDelegate func) {
        if(_subscribers.Count == 0) {
            await this.Register();
        }
        var token = (++subUid).ToString();
        _subscribers.Add(new KeyValuePair<string, SubscribeDelegate> (token, func));
        func.Invoke(screens, null);
    }

    public async Task Unsubscribe(string token) {
        foreach(var i in Enumerable.Range(0, _subscribers.Count).Reverse()) {
            var item = _subscribers[i];
            if(item.Key == token) {
                _ = _subscribers.Remove(item);
            }
        }
        if(_subscribers.Count == 0) {
            await this.Unregister();
        }
    }

    public bool Dispatch(ScreenMap pointMap, Breakpoint breakpointChecked) {
        screens = pointMap;
        if (_subscribers.Count < 1) {
            return false;
        }
        foreach (var item in _subscribers) {
            item.Value.Invoke(screens, breakpointChecked);
        }
        return true;
    }

    private async Task AddListener(string matchMediaQuery, Action<bool> listener) {
        await jSRuntime.InvokeVoidAsync("window.AddListener", matchMediaQuery, listener);
    }

    private async Task RemoveListener(string matchMediaQuery, Action<bool> listener) {
        await jSRuntime.InvokeVoidAsync("window.RemoveListener", matchMediaQuery, listener);
    }

}

internal class MediaQueryList {
    [JsonPropertyName("media")]
    public string Media { get; set; }
    [JsonPropertyName("matches")]
    public bool Matches { get; set; }
}

internal class MediaQueryMap {
    public required MediaQueryList MediaQuery { get; set; }
    public required Action<bool> Listener { get; set; }
}