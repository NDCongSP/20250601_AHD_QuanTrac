using Application.DTOs.Response.Account;

namespace UI.Pages.Roles;

public partial class RoleManager
{
    List<GetRoleResponseDTO> _dataGrid = null;
    RadzenDataGrid<GetRoleResponseDTO> _profileGrid;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        LayoutState.SetTitle("QUẢN LÝ PHÂN QUYỀN");
        await RefreshDataAsync();
    }

    async Task EditItemAsync(string id)
    {
        _navigation.NavigateTo($"/role-detail&id={id}");
    }

    async Task AddNewItemAsync()
    {
        _navigation.NavigateTo($"/role-detail");
    }

    async Task ViewItemAsync(string id)
    {
    }

    async Task RefreshDataAsync()
    {
        try
        {
            var res = await _accountServices.GetRolesAsync();

            if (res == null)
            {
                _notificationService.Notify(new NotificationMessage()
                {
                    Severity = NotificationSeverity.Error,
                    Summary = _localizer["Error"],
                    Detail = _localizer["Something went wrong."],
                });
                return;
            }

            
            _dataGrid = null;
            _dataGrid = new List<GetRoleResponseDTO>();
            _dataGrid = res;

            StateHasChanged();
        }
        catch (UnauthorizedAccessException) { }
        catch (Exception ex)
        {
            NotificationHelper.ShowNotification(_notificationService, NotificationSeverity.Error, _localizer["Error"], ex.Message);
            return;
        }
    }
}
