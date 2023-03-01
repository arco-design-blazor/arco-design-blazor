using ArcoDesign.Extensions;
using ArcoDesign.Shared;
using Microsoft.AspNetCore.Components;

using GridRowGutter = OneOf.OneOf<int, System.Collections.Generic.Dictionary<string, int>>;

namespace ArcoDesign.Components;


public partial class Row {

    private readonly string prefixCls = "arco-row";
    private readonly string[] _responsiveArray = new[] { "xxxl", "xxl", "xl", "lg", "md", "sm", "xs" };
    private readonly Dictionary<string, bool> screens = new Dictionary<string, bool>
    {
        {"xs", true },
        {"sm", true },
        {"md", true },
        {"lg", true },
        {"xl", true },
        {"xxl", true },
        {"xxxl", true },
    };

    [Parameter]
    public OneOf.OneOf<GridRowGutter, GridRowGutter[]> Gutter { get; set; }

    [Parameter]
    public bool Div { get; set; } = false;

    [Parameter]
    public Align Align { get; set; } = Align.Start;

    [Parameter]
    public Justify Justify { get; set; } = Justify.Start;

    [Parameter]
    public RenderFragment ChildContent { get; set; }

    protected override void OnParametersSet() {
        base.OnParametersSet();
    }


    protected override void OnInitialized() {
        _ = classNameBuilder
            .AddIf(!Div, prefixCls)
            .Add($"{prefixCls}-align-{Align.ToEnumName()}")
            .Add($"{prefixCls}-justify-{Justify.ToEnumName()}")
            .AddIf(Rtl.HasValue && Rtl.Value, $"{prefixCls}-rtl");

        var gutterHorizontal = GetGutter(Gutter.Match(
             num => num,
             list => list[0])
            );

        var gutterVertical = GetGutter(Gutter.Match(
             _ => 0,
             list => list[1]));
        int? marginTop = null;
        int? marginBottom = null;
        int? marginLeft = null;
        int? marginRight = null;
        if((gutterHorizontal != 0 || gutterVertical != 0) && !Div) {
            var marginHorizontal = (gutterHorizontal * -1) / 2;
            var marginVertical = (gutterVertical * -1) / 2;
            if(marginHorizontal != 0) {
                marginLeft= marginHorizontal;
                marginRight = marginHorizontal;
            }
            if(marginVertical != 0) {
                marginTop = marginVertical;
                marginBottom = marginVertical;
            }
        }

        _ = styleBuilder
            .AddIf(marginTop.HasValue, ("margin-top", $"{marginTop.Value}px"))
            .AddIf(marginBottom.HasValue, ("margin-bottom", $"{marginBottom.Value}px"))
            .AddIf(marginLeft.HasValue, ("margin-left", $"{marginLeft.Value}px"))
            .AddIf(marginRight.HasValue, ("margin-right", $"{marginRight.Value}px"));
        
        base.OnInitialized();
    }


    internal int GetGutter(GridRowGutter gutter) {
        return gutter.Match(
             num => num,
              dict => {
                  foreach (var item in _responsiveArray) {
                      if (screens.TryGetValue(item, out var value) && value && dict.TryGetValue(item, out var t)) {
                          return t;
                      }
                  }
                  return 0;
              }
        );
    }

}
