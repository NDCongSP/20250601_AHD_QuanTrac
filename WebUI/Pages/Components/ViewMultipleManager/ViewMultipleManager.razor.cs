using Microsoft.AspNetCore.Components;
using WebUI.Pages.Components.ViewMultipleManager;

namespace UI.Components.ViewMultipleManager;

public partial class ViewMultipleManager<TItem> : ComponentBase
{
    [Parameter] public RenderFragment<ListViewContext<TItem>> ListView { get; set; }
    [Parameter] public Dictionary<DetailViewType, RenderFragment<DetailViewContext<TItem>>> DetailViews { get; set; }
    [Parameter] public EventCallback OnReturnToList { get; set; }
    [Parameter] public bool NeedsRefresh { get; set; }
    [Parameter] public EventCallback<bool> NeedsRefreshChanged { get; set; }

    private bool _showList = true;
    private bool _showDetail = false;
    private string _detailTitle = string.Empty;
    private TItem _selectedItem = default;
    private string? _selectedId = null;
    private DetailViewType? _currentDetailViewType;

    private ListViewContext<TItem> _listContext;
    private DetailViewContext<TItem> _detailContext;

    protected override void OnInitialized()
    {
        base.OnInitialized();

        _listContext = new ListViewContext<TItem>
        {
            OnAddNew = (data, type) => ShowDetail(data, type),
            OnEdit = (data, type) => ShowDetail(data, type),
            OnView = (data, type) => ShowDetail(data, type),
            NeedsRefresh = NeedsRefresh
        };

        _detailContext = new DetailViewContext<TItem>
        {
            Item = _selectedItem,
            Title = _detailTitle,
            OnSaved = HandleDetailSaved,
            OnDelete = HandleDelete,
            OnCancelled = HandleDetailCancelled
        };
    }

    public void ShowList()
    {
        _showList = true;
        _showDetail = false;
        _selectedItem = default;
        _selectedId = null;
        _currentDetailViewType = null;
        NeedsRefresh = false;
        StateHasChanged();
    }

    public void ShowDetail(DetailViewData data, DetailViewType type, TItem item = default)
    {
        _showList = false;
        _showDetail = true;
        NeedsRefresh = false;

        _selectedId = data.Id;
        _detailTitle = _detailContext.Title = data.Title;
        _selectedItem = _detailContext.Item = item;
        _currentDetailViewType = type;

        StateHasChanged();
    }

    private async Task HandleDetailSaved(bool isNeedRefresh)
    {
        NeedsRefresh = isNeedRefresh;
        await NeedsRefreshChanged.InvokeAsync(NeedsRefresh);
        ShowList();
    }

    private async Task HandleDelete(TItem item)
    {
        NeedsRefresh = true;
        await NeedsRefreshChanged.InvokeAsync(NeedsRefresh);
        ShowList();
    }

    private async Task HandleDetailCancelled(bool isNeedRefresh)
    {
        NeedsRefresh = isNeedRefresh;
        await NeedsRefreshChanged.InvokeAsync(NeedsRefresh);
        await OnReturnToList.InvokeAsync();
        ShowList();
    }
}
