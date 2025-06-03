using Microsoft.AspNetCore.Components;

namespace WebUI.Models;

public abstract class ListBaseComponent<TItem> : ComponentBase
{
    protected IEnumerable<int> _pageSizeOptions = new int[] { 5, 10, 20, 30, 100, 200 };
    protected string _pagingSummaryFormat = string.Empty;
    protected bool _showPagerSummary = true;

    [Parameter] public EventCallback<DetailViewData> OnAddNew { get; set; }
    [Parameter] public EventCallback<DetailViewData> OnEdit { get; set; }
    [Parameter] public EventCallback<DetailViewData> OnView { get; set; }
    [Parameter] public EventCallback<TItem> OnDelete { get; set; }
    [Parameter] public bool NeedsRefresh { get; set; }
    [Parameter] public bool _isFirstRender { get; set; } = true;
    public virtual Task LoadDataAsync() => Task.CompletedTask;

    protected override async Task OnParametersSetAsync()
    {
        if (NeedsRefresh)
        {
            await LoadDataAsync();
        }
    }
}
