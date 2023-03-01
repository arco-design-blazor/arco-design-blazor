using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace ArcoDesign.Components.Typography;
public abstract class OperationBase: ArcoDesignComponentBase {
    protected readonly Dictionary<string, object> currentContext = new Dictionary<string, object>();

    [Parameter]
    public bool Copyable { get; set; }

    [Parameter]
    public RenderFragment CopyableNode { get; set; }
    [Parameter]
    public RenderFragment EditableNode { get; set; }
    [Parameter]
    public bool Editable { get; set; }
    [Parameter]
    public bool Ellipsis { get; set; }
    [Parameter]
    public bool IsEllipsis { get; set; }
    [Parameter]
    public bool Expanding { get; set; }
    [Parameter]
    public bool ForceShowExpand { get; set; }
    [Parameter]
    public EventCallback<MouseEventArgs> OnClickExpand { get; set; }
    [Parameter]
    public EventCallback<bool> SetEditing { get; set; }
}
public class CopyableNode : ArcoDesignComponentBase {
    [Parameter]
    public string Text { get; set; }

    [Parameter]
    public EventCallback<string> OnCopy { get; set; }

    [Parameter]
    public RenderFragment IconNode { get; set; }
    [Parameter]
    public RenderFragment LeftToolTip { get; set; }
    [Parameter]
    public RenderFragment RightToolTip { get; set; }
}

public class EditableNode : ArcoDesignComponentBase {
    public bool Editing { get; set; }
    public EventCallback<string> OnStart { get; set; }
    public EventCallback<string> OnChange { get; set; }
    public EventCallback<string> OnEnd { get; set; }
}