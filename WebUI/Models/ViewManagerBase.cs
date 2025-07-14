using Microsoft.AspNetCore.Components;

namespace UI.Models;

public class ViewManagerBase : ComponentBase
{
    protected bool _needsRefresh = false;
    protected string? _selectedUnitId = null;
    protected string? _title = null;

    protected async Task HandleReturnToList()
    {
        //_needsRefresh = true;
        _selectedUnitId = null;
        StateHasChanged();
    }

    private void HandleItemSelected(DetailViewData data)
    {
        _selectedUnitId = data.Id;
        _title = data.Title;
    }
    public async Task HandleNeedsRefreshChanged(bool needsRefresh)
    {
        _needsRefresh = needsRefresh;
        StateHasChanged();
    }
}
