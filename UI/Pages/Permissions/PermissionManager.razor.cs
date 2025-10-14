using Application.DTOs.Response.Account;
using Newtonsoft.Json;

namespace UI.Pages.Permissions;

public partial class PermissionManager
{
    List<PermissionsListResponseDTO> _dataGrid = null;
    RadzenDataGrid<PermissionsListResponseDTO> _profileGrid;
    public static IEnumerable<int> PageSizeOptions = [20, 30, 100, 200];

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        LayoutState.SetTitle(_localizer["PermissionManager.Title"]);
        await RefreshDataAsync();
    }

    async Task DeleteItemAsync(PermissionsListResponseDTO model)
    {
        try
        {
            var confirm = await _dialogService.Confirm(_localizer["Confirmation.Delete"] + _localizer["Permission.Name"] + $": {model.Name}?", _localizer["Delete"] + " " + _localizer["Permission.Name"], new ConfirmOptions()
            {
                OkButtonText = _localizer["Yes"],
                CancelButtonText = _localizer["No"],
                AutoFocusFirstElement = true,
            });

            if (confirm == null || confirm == false) return;

            var res = await _permissionsServices.DeleteAsync(model);

            if (res.Succeeded)
            {
                _notificationService.Notify(new NotificationMessage()
                {
                    Severity = NotificationSeverity.Success,
                    Summary = _localizer["Success"],
                    Detail = $"Delete permission {model.Name} successfully.",
                    Duration = 5000
                });

                RefreshDataAsync();
            }
            else
            {
                _notificationService.Notify(new NotificationMessage()
                {
                    Severity = NotificationSeverity.Error,
                    Summary = _localizer["Error"],
                    Detail = res.Messages.ToString(),
                    Duration = 5000
                });
            }
        }
        catch (Exception ex)
        {
            _notificationService.Notify(new NotificationMessage()
            {
                Severity = NotificationSeverity.Error,
                Summary = _localizer["Error"],
                Detail = ex.Message,
                Duration = 5000
            });

            return;
        }
    }

    async Task EditItemAsync(string id)
    {
        _navigation.NavigateTo($"/permission-detail/{id}");
    }

    async Task AddNewItemAsync()
    {
        _navigation.NavigateTo($"/permission-detail");
    }

    async Task ViewItemAsync(string id)
    {
    }

    async Task RefreshDataAsync()
    {
        try
        {
            var res = await _permissionsServices.GetAllPermissionWithAssignedRoleAsync();

            if (!res.Succeeded)
            {
                var error = JsonConvert.DeserializeObject<ErrorResponse>(res.Messages.FirstOrDefault())?.Errors.FirstOrDefault();

                NotificationHelper.ShowNotification(_notificationService
                   , error?.Key == "Warning" ? NotificationSeverity.Warning : NotificationSeverity.Error
                   , _localizer[error?.Key], _localizer[error?.Value]);

                return;
            }

            _dataGrid = null;
            _dataGrid = new List<PermissionsListResponseDTO>();
            _dataGrid = res.Data.ToList();

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
