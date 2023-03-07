using ArcoDesign.Core;
using ArcoDesign.Extensions;
using ArcoDesign.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Text.Json.Serialization;

using GridRowGutter = OneOf.OneOf<int, int[], System.Collections.Generic.Dictionary<ArcoDesign.Core.Breakpoint, int>>;

namespace ArcoDesign.Components;


public partial class Row : IAsyncDisposable {

    private readonly string prefixCls = "arco-row";
    private Dictionary<Breakpoint, bool> screens = new Dictionary<Breakpoint, bool>
    {
        {Breakpoint.XS, true },
        {Breakpoint.SM, true },
        {Breakpoint.MD, true },
        {Breakpoint.LG, true },
        {Breakpoint.XL, true },
        {Breakpoint.XXL, true },
        {Breakpoint.XXXL, true },
    };

    [Inject]
    private IJSRuntime Js { get; set; }

    private IJSObjectReference? reposeObserveModule;
    private string subscribeToken;
    private DotNetObjectReference<Row> currentInstance;

    /// <summary>
    /// int 类型
    /// </summary>
    [Parameter]
    public GridRowGutter Gutter { get; set; }

    [Parameter]
    public bool Div { get; set; } = false;

    [Parameter]
    public Align Align { get; set; } = Align.Start;

    [Parameter]
    public Justify Justify { get; set; } = Justify.Start;

    [Parameter]
    public RenderFragment ChildContent { get; set; }

    protected override void OnParametersSet() {
        _ = classNameBuilder
            .AddIf(!Div, prefixCls)
            .Add($"{prefixCls}-align-{Align.ToEnumName()}")
            .Add($"{prefixCls}-justify-{Justify.ToEnumName()}")
            .AddIf(Rtl.HasValue && Rtl.Value, $"{prefixCls}-rtl");

        var gutterHorizontal = HorizontalGutter;
        var gutterVertical = VerticalGutter;
        int? marginTop = null;
        int? marginBottom = null;
        int? marginLeft = null;
        int? marginRight = null;
        if ((gutterHorizontal != 0 || gutterVertical != 0) && !Div) {
            var marginHorizontal = gutterHorizontal * -1 / 2;
            var marginVertical = gutterVertical * -1 / 2;
            if (marginHorizontal != 0) {
                marginLeft = marginHorizontal;
                marginRight = marginHorizontal;
            }
            if (marginVertical != 0) {
                marginTop = marginVertical;
                marginBottom = marginVertical;
            }
        }
        _ = styleBuilder
            .AddIf(marginTop != null && marginTop.HasValue, ("margin-top", $"{marginTop}px"))
            .AddIf(marginBottom != null && marginBottom.HasValue, ("margin-bottom", $"{marginBottom}px"))
            .AddIf(marginLeft != null && marginLeft.HasValue, ("margin-left", $"{marginLeft}px"))
            .AddIf(marginRight != null && marginRight.HasValue, ("margin-right", $"{marginRight}px"));

        base.OnParametersSet();
    }

    protected override void OnInitialized() {
        currentInstance = DotNetObjectReference.Create(this);

        base.OnInitialized();
    }


    protected override async Task OnAfterRenderAsync(bool firstRender) {
        await base.OnAfterRenderAsync(firstRender);
        if(firstRender) {
            reposeObserveModule = await Js.InvokeAsync<IJSObjectReference>("import", "./_content/ArcoDesign/scripts/responsiveObject.js");
            subscribeToken = await reposeObserveModule.InvokeAsync<string>("subscribe", currentInstance);
        }

    }
    [JSInvokable]
    public void ResponsiveHandle(Dictionary<Breakpoint, bool> screens, string _) {

        if(Gutter.IsT2 || Gutter.IsT1) {
            this.screens = screens;
        }
    }

    internal int HorizontalGutter {
        get {
            return Gutter.Match(
                f0 => f0,
                f2 => f2[0],
                dict => {
                    foreach (Breakpoint item in Enum.GetValues(typeof(Breakpoint))) {
                        if (screens.TryGetValue(item, out var value) && value && dict.TryGetValue(item, out var t)) {
                            return t;
                        }
                    }
                    return 0;
                });
        }
    }

    internal int VerticalGutter {
        get {
            return Gutter.Match(
                f0 => f0,
                f2 => f2[1],
                dict => {
                    foreach (Breakpoint item in Enum.GetValues(typeof(Breakpoint))) {
                        if (screens.TryGetValue(item, out var value) && value && dict.TryGetValue(item, out var t)) {
                            return t;
                        }
                    }
                    return 0;
                });
        }
    }

    public async ValueTask DisposeAsync() {
        if(reposeObserveModule is not null) {
            await reposeObserveModule.InvokeVoidAsync("unsubscribe", subscribeToken);
            await reposeObserveModule.DisposeAsync();
            reposeObserveModule = null;
        }
    }
}
public record ScreenMap {
    [JsonPropertyName("xxxl")]
    public bool XXXL { get; set; }
    [JsonPropertyName("xxl")]
    public bool XXL { get; set; }
    [JsonPropertyName("xl")]
    public bool XL { get; set; }
    [JsonPropertyName("lg")]
    public bool LG { get; set; }
    [JsonPropertyName("md")]
    public bool MD { get; set; }
    [JsonPropertyName("sm")]
    public bool SM { get; set; }
    [JsonPropertyName("xs")]
    public bool XS { get; set; }
}