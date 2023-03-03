﻿using ArcoDesign.Core;
using ArcoDesign.Extensions;
using Microsoft.AspNetCore.Components;

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
    protected string classNames {
        get {
            return this.classNameBuilder.AddIf(ClassName.IsNotNullOrEmpty(), ClassName).Build();
        }
    }

    /// <summary>
    /// 组件挂载使用 style
    /// </summary>
    protected string styles {
        get {
            var data = this.styleBuilder.Build();
            if (data.IsNullOrEmpty()) {
                return Style;
            }
            return $"{data};{Style}";
        }
    }

    /// <summary>
    /// 组件挂载使用 attrbutes
    /// </summary>
    protected string attributes {
        get {
            return this.AttributeBuilder.Build();
        }
    }

    /// <summary>
    /// 外部传入组件 style
    /// </summary>
    [Parameter]
    public string Style { get; set; }

    /// <summary>
    /// 外部传入 class
    /// </summary>
    [Parameter]
    public string ClassName { get; set; }
}
