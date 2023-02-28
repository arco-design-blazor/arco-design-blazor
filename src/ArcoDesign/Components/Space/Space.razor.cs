using ArcoDesign.Extensions;
using ArcoDesign.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcoDesign.Components;

public partial class Space {
    private readonly string prefixCls = "arco-space";

    [Parameter]
    public Align? Align { get; set; }

    [Parameter]
    public Direction Direction { get; set; } = Direction.Horizontal;

    [Parameter]
    public RenderFragment Split { get; set; }

    [Parameter]
    public RenderFragment ChildContent { get; set; }

    /// <summary>
    /// mini small default medium large  number.
    /// </summary>
    [Parameter]
    public string Size { get; set; } = "default";

    [Parameter]
    public bool Wrap { get; set; } = false;

    internal int ItemsCount { get; set; }
    internal bool ShouldRenderSplit => !Split.IsNullOrEmpty();

    protected override void OnInitialized() {
        var innerAlign = Align ?? (Direction == Direction.Horizontal ? Shared.Align.Center : Shared.Align.Start);

        _ = classNameBuilder.Add(prefixCls)
            .Add($"{prefixCls}-align-{innerAlign.ToEnumName()}")
            .Add($"{prefixCls}-{Direction.ToEnumName()}")
            .AddIf(Wrap, $"{prefixCls}-wrap")
            .AddIf(Rtl.HasValue && Rtl.Value, $"{prefixCls}-rtl");

        base.OnInitialized();
    }

    private decimal GetMargin(string size) {
        if (size.IsNumeric()) {
            return Convert.ToDecimal(size);
        }
        return Size switch {
            "mini" => 4,
            "small" => 8,
            "medium" => 16,
            "large" => 24,
            _ => 8
        };
    }

    internal string GetMarginStyle(int index) {
        var isLastOne = ItemsCount == index + 1;
        var marginDirection = Rtl.HasValue && Rtl.Value ? "margin-left" : "margin-right";

        if (Size.StartsWith('[') && Size.EndsWith(']')) {
            var s = Size[1..-1];
            var arr = s.Split(',');
            var marginHorizontal = GetMargin(arr[0]);
            var marginBottom = GetMargin(arr[1]);
            if (Wrap) {
                return isLastOne ? $"margin-bottom: {marginBottom}px;" : $"{marginDirection}:{marginHorizontal}px;margin-bottom: {marginBottom}px;";
            }
            if (Direction == Direction.Vertical) {
                return $"margin-bottom: {marginBottom}";
            }
            return $"{marginDirection}:{marginHorizontal}";
        }

        var margin = GetMargin(Size);
        if (Wrap) {
            return isLastOne ? $"margin-bottom: {margin}px;" : $"{marginDirection}:{margin}px;margin-bottom: {margin}px;";
        }
        if (!isLastOne) {
            if (Direction == Direction.Vertical) {
                return $"margin-bottom:{margin}px;";
            }
            return $"{marginDirection}:{margin}px;";
        }
        return "";
    }
}
