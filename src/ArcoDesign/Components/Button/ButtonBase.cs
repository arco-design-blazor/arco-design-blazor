using ArcoDesign.Infra;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace ArcoDesign.Components;

public abstract class ButtonBase : BlazorComponentBase {

    [Parameter]
    public ButtonType Type { get; set; }

    [Parameter]
    public ButtonStatus Status { get; set; } = ButtonStatus.Default;

    [Parameter]
    public ButtonShape Shape { get; set; } = ButtonShape.None;

    [Parameter]
    public string Href { get; set; } = string.Empty;

    [Parameter]
    public string Target { get; set; } = string.Empty;

    [Parameter]
    public bool Disabled { get; set; } = false;

    [Parameter]
    public bool Loading { get; set; } = false;
    [Parameter]
    public bool LoadingFixedWidth { get; set; } = false;

    [Parameter]
    public bool IconOnly { get; set; } = false;
    public RenderFragment? Icon { get; set; }

    [Parameter]
    public bool Long { get; set; } = false;

    [Parameter]
    public EventCallback<MouseEventArgs>? OnClick { get; set; }

    [Parameter]
    public RenderFragment ChildContent { get; set; }

    [Parameter]
    public Size Size { get; set; } = Size.Default;
}

public enum ButtonType {
    Default = 0,
    Primary = 1,
    Secondary = 2,
    Dashed = 3,
    Text = 4,
    Outline = 5,
}

public enum ButtonStatus {
    Default = 0,
    Warning = 1,
    Danger = 2,
    Success = 3,
}

public enum ButtonShape {
    None = 0,
    Circle = 1,
    Round = 2,
    Square = 3,
}