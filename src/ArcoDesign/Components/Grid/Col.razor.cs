using ArcoDesign.Core;
using ArcoDesign.Extensions;
using Microsoft.AspNetCore.Components;
using OneOf;
using System.Text.RegularExpressions;


namespace ArcoDesign.Components;


public partial class Col {
    private readonly string prefixCls = "arco-col";

    [CascadingParameter]
    public Row Row { get; set; }

    [Parameter]
    public int Span { get; set; } = 0;

    [Parameter]
    public int? Offset { get; set; } 
    [Parameter]
    public int? Order { get; set; }

    [Parameter]
    public RenderFragment ChildContent { get; set; }

    [Parameter]
    public int Push { get; set; }
    [Parameter]
    public int Pull { get; set; }
    [Parameter]
    public OneOf<double, ColProperties>? XS { get; set; }
    [Parameter]
    public OneOf<double, ColProperties>? SM { get; set; }
    [Parameter]
    public OneOf<double, ColProperties>? MD { get; set; }
    [Parameter]
    public OneOf<double, ColProperties>? LG { get; set; }
    [Parameter]
    public OneOf<double, ColProperties>? XL { get; set; }
    [Parameter]
    public OneOf<double, ColProperties>? XXL { get; set; }
    [Parameter]
    public OneOf<double, ColProperties>? XXXL { get; set; }
    /// <summary>
    /// auto none (number / string)
    /// </summary>
    [Parameter]
    public OneOf<string, double>? Flex { get; set; }

    protected override void OnParametersSet() {
        classNameBuilder.Clear();
        styleBuilder.Clear();
        _ = classNameBuilder
            .AddIf(!Row.Div, $"{prefixCls}")
            .AddIf(Order.HasValue, $"{prefixCls}-order-${Order}")
            .AddIf(!Row.Div && !XS.HasValue && !SM.HasValue && !MD.HasValue && !LG.HasValue && !XL.HasValue && !XXL.HasValue && !XXXL.HasValue, $"{prefixCls}-{Span}")
            .AddIf(Offset.HasValue, $"{prefixCls}-offset-{Offset}")
            .AddIf(Pull != 0, $"{prefixCls}-pull-{Pull}")
            .AddIf(Push != 0, $"{prefixCls}-push-{Push}")
            //.AddIf(Span != 0, $"{prefixCls}-span-{Span}")
            .AddIf(Rtl.HasValue && Rtl.Value, $"{prefixCls}-rtl");
        
        AdaptationGrid();

        var flexStyle = getFlexString(Flex);
        _ = styleBuilder.AddIf(flexStyle.IsNotNullOrEmpty(), ("flex", flexStyle));

        int? paddingTop = null;
        int? paddingBottom = null;
        int? paddingLeft = null;
        int? paddingRight = null;

        if(Row.Gutter.IsT1 && !Row.Div) {
            var gutter = Row.Gutter.AsT1;

            var paddingHorizontal = gutter[0];
            var paddingVertical = gutter[1];
            if(paddingHorizontal != 0) {
                paddingHorizontal = gutter[0] / 2;
                paddingLeft = paddingHorizontal;
                paddingRight = paddingHorizontal;
            }
            if(paddingVertical != 0) {
                paddingVertical = paddingVertical / 2;
                paddingTop= paddingVertical;
                paddingBottom= paddingVertical;
            }
        }
        if (Row.Gutter.IsT0&& !Row.Div) {
            var gutter = Row.Gutter.AsT0;

            var paddingHorizontal = gutter / 2;
            var paddingVertical = gutter / 2;
            if (paddingHorizontal != 0) {
                paddingLeft = paddingHorizontal;
                paddingRight = paddingHorizontal;
            }
            if (paddingVertical != 0) {
                paddingVertical = paddingVertical / 2;
                paddingTop = paddingVertical;
                paddingBottom = paddingVertical;
            }
        }

        _ = styleBuilder
            .AddIf(paddingLeft.HasValue, ("padding-left", $"{paddingLeft}"))
            .AddIf(paddingRight.HasValue, ("padding-right", $"{paddingRight}"))
            .AddIf(paddingTop.HasValue, ("padding-top", $"{paddingTop}"))
            .AddIf(paddingBottom.HasValue, ("padding-bottom", $"{paddingBottom}"));

        base.OnParametersSet();
    }

    private void AdaptationGrid() {
        var properties = this.GetType().GetProperties();

        foreach (var breakpointName in Enum.GetNames<Breakpoint>()) {
            var property = properties.FirstOrDefault(p => string.Compare(p.Name, breakpointName, true)==0);
            if(property is null) {
                continue;
            }
            if(property.GetValue(this) is OneOf<double, ColProperties> value) {
                if (value.IsT0 && value.AsT0 >= 0) {
                    _ = classNameBuilder.Add($"{prefixCls}-{breakpointName.ToLower()}-${value.AsT0}");
                } else if(value.IsT1) {
                    var v2 = value.AsT1;
                    if (v2 != null) {

                       _ = classNameBuilder.AddIf(v2.Span != 0, $"{prefixCls}-{breakpointName.ToLower()}-${v2.Span}")
                            .AddIf(v2.Offset != 0, $"{prefixCls}-{breakpointName.ToLower()}-offset-${v2.Offset}")
                            .AddIf(v2.Order != 0, $"{prefixCls}-{breakpointName.ToLower()}-order-${v2.Order}")
                            .AddIf(v2.Pull != 0, $"{prefixCls}-{breakpointName.ToLower()}-pull-${v2.Pull}")
                            .AddIf(v2.Push != 0, $"{prefixCls}-{breakpointName.ToLower()}-push-${v2.Push}");
                    }
                }
            }
        }
    }

    private static readonly string flexPattern = @"\d+[px|%|em|rem|]{1}";
    private static string? getFlexString(OneOf<string, double>? flex) {
        if(flex is null) {
            return null;
        }

        if (flex.Value.IsT0) {
            var value = flex.Value.AsT0;
            if(Regex.IsMatch(value, flexPattern)) {
                return $"0 0 {value}";
            }
            return value;
        }
        return flex.Value.AsT1.ToString();
    }

}

public class ColProperties {
    public int Span { get; set; }
    public int Offset { get; set; }
    public int Order { get; set; }
    public int Pull { get; set; }
    public int Push { get; set; }
}
