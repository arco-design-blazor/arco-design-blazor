using ArcoDesign.Components.Typography;
using Microsoft.AspNetCore.Components;

namespace ArcoDesign.Components;

public abstract class TypographyBase : OperationBase {
    /// <summary>
    /// 内容项
    /// </summary>
    [Parameter]
    public RenderFragment ChildContent { get; set; }

    /// <summary>
    /// 文本类型
    /// </summary>
    [Parameter]
    public TextType Type { get; set; }

    /// <summary>
    /// 加粗
    /// </summary>
    [Parameter]
    public bool Bold { get; set; } = false;

    /// <summary>
    /// 禁用
    /// </summary>
    [Parameter]
    public bool Disabled { get; set; } = false;

    /// <summary>
    /// 标记
    /// </summary>
    [Parameter]
    public bool Mark { get; set; }

    /// <summary>
    /// 下划线
    /// </summary>
    [Parameter]
    public bool Underline { get; set; } = false;

    /// <summary>
    /// 删除线
    /// </summary>
    [Parameter]
    public bool Delete { get; set; } = false;

    /// <summary>
    /// 代码块
    /// </summary>
    [Parameter] 
    public bool Code { get; set; } = false; 
}

public enum TextType {
    Primary,
    Secondary,
    Success,
    Error,
    Warning
}
