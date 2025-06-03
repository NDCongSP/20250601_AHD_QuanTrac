using Microsoft.AspNetCore.Components;

namespace WebUI.Models;

public class DetailBaseComponent : ComponentBase
{
    [Parameter] public string Title { get; set; }
    [Parameter] public string? Id { get; set; }
    [Parameter] public EventCallback<bool> OnSaved { get; set; }
    [Parameter] public EventCallback<bool> OnCancelled { get; set; }
    public bool IsNeedsRefresh { get; set; } = false;
}
