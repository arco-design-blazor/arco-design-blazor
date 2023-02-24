using Microsoft.AspNetCore.Components;

namespace ArcoDesign;

/// <summary>
/// arco 组件基类
/// </summary>
public abstract class ArcoDesignComponentBase : BlazorComponentBase {
    [CascadingParameter]
    public bool? Rtl { get; set; }
}
