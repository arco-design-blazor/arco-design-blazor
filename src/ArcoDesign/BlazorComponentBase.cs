using ArcoDesign.Extensions;
using ArcoDesign.Infra.CssBuilders;
using ArcoDesign.Infra.JsRuntimes;
using Microsoft.AspNetCore.Components;
using System.Collections.Specialized;

namespace ArcoDesign;
public abstract class BlazorComponentBase : ComponentBase {

    /// <summary>
    /// js
    /// </summary>
    [Inject]
    internal Js Js { get; set; }

    /// <summary>
    /// 组件 style 构建器
    /// </summary>
    protected readonly StyleBuilder styleBuilder = new StyleBuilder();

    /// <summary>
    /// 组件 class 构建器
    /// </summary>
    protected readonly ClassNameBuilder classNameBuilder = new ClassNameBuilder();

    /// <summary>
    /// 组件 attribute 构建器
    /// </summary>
    protected readonly AttributeBuilder AttributeBuilder = new AttributeBuilder();

    /// <summary>
    /// 组件挂载使用 class
    /// </summary>
    protected string classNames => this.classNameBuilder.Build();

    /// <summary>
    /// 组件挂载使用 style
    /// </summary>
    protected string styles => this.styleBuilder.Build();

    /// <summary>
    /// 组件挂载使用 attrbutes
    /// </summary>
    protected string attributes => this.AttributeBuilder.Build();

    /// <summary>
    /// 外部传入组件 style
    /// </summary>
    [Parameter]
    public object Style { get; set; }

    /// <summary>
    /// 外部传入 class
    /// </summary>
    [Parameter]
    public string ClassName { get; set; }
}
