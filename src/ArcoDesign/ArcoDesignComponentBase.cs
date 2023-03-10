using ArcoDesign.Core;
using ArcoDesign.Extensions;
using Microsoft.AspNetCore.Components;

namespace ArcoDesign;

/// <summary>
/// arco 组件基类
/// </summary>
public abstract class ArcoDesignComponentBase : ComponentBase {

    /// <summary>
    /// 组件 style 构建器
    /// </summary>
    private readonly StyleBuilder _styleBuilder = new StyleBuilder();

    /// <summary>
    /// 组件 class 构建器
    /// </summary>
    private readonly ClassNameBuilder _classNameBuilder = new ClassNameBuilder();

    /// <summary>
    /// 组件 attribute 构建器
    /// </summary>
    private readonly AttributeBuilder _attributeBuilder = new AttributeBuilder();

    /// <summary>
    /// js
    /// </summary>
    [Inject]
    internal Js Js { get; set; }

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
    [CascadingParameter]
    public bool? Rtl { get; set; }

    /// <summary>
    /// 组件挂载使用 class
    /// </summary>
    protected string classNames {
        get {
            return this._classNameBuilder.AddIf(ClassName.IsNotNullOrEmpty(), ClassName).Build();
        }
    }

    /// <summary>
    /// 组件挂载使用 style
    /// </summary>
    protected string styles {
        get {
            return this._styleBuilder.AddIfNotNullOrEmpty(Style).Build();
        }
    }

    /// <summary>
    /// 组件挂载使用 attrbutes
    /// </summary>
    protected string attributes {
        get {
            return this._attributeBuilder.Build();
        }
    }

    protected override void OnParametersSet() {
        _classNameBuilder.RemoveAll();
        _styleBuilder.RemoveAll();
        _attributeBuilder.RemoveAll();
        base.OnParametersSet();
        SetComponentCss(_classNameBuilder, _styleBuilder, _attributeBuilder);
    }

    /// <summary>
    /// 数据变动主动刷新样式。
    /// </summary>
    /// <param name="classNameBuilder">类名构建</param>
    /// <param name="styleBuilder">样式构建</param>
    /// <param name="attributeBuilder">属性构建</param>
    protected virtual void SetComponentCss(ClassNameBuilder classNameBuilder, StyleBuilder styleBuilder, AttributeBuilder attributeBuilder) {
    }
}
