using Application.DTOs.Response;
using Application.DTOs.Response.Account;

using Newtonsoft.Json;
using RestEase;
using WebUI.Models;

namespace WebUI.Pages.Account;

public partial class Permission : ListBaseComponent<PermissionsListResponseDTO>
{
    List<PermissionsListResponseDTO> _dataGrid = null;
    RadzenDataGrid<PermissionsListResponseDTO> _profileGrid;
    
    protected override async Task OnInitializedAsync()
    {
        if (_isFirstRender)
        {
            await base.OnInitializedAsync();
            _pagingSummaryFormat = _localizer["DisplayPage"] + " {0} " + _localizer["Of"] + " {1} <b>(" + _localizer["Total"] + " {2} " + _localizer["Records"] + ")</b>";
            await RefreshDataAsync();
            _isFirstRender = false;
        }
    }

    public override async Task LoadDataAsync()
    {
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
        await OnEdit.InvokeAsync(new Models.DetailViewData($"{_localizer["Detail.Edit"]} {_localizer["Permission"]}|{id}", id));
    }

    async Task AddNewItemAsync()
    {
        await OnAddNew.InvokeAsync(new Models.DetailViewData($"{_localizer["Detail.Create"]} {_localizer["Permission"]}"));
    }

    async Task ViewItemAsync(string id)
    {
        await OnView.InvokeAsync(new Models.DetailViewData($"{_localizer["Detail.View"]} {_localizer["Permission"]}|{id}", id));
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
                   , _localizerNotification[error?.Key], _localizerNotification[error?.Value]);

                return;
            }

            _dataGrid = null;
            _dataGrid = new List<PermissionsListResponseDTO>();
            _dataGrid = res.Data.ToList();

            StateHasChanged();
        }
        catch (UnauthorizedAccessException) { }
        catch (ApiException ex)
        {
            ApiErrorResponse errorResponse = null;

            if (ex.Content != null)
            {
                errorResponse = JsonConvert.DeserializeObject<ApiErrorResponse>(ex.Content.ToString());
            }

            NotificationHelper.ShowNotification(_notificationService, NotificationSeverity.Error, _localizerNotification["Error"], _localizerNotification[errorResponse?.error]);
            return;
        }
        catch (Exception ex)
        {
            NotificationHelper.ShowNotification(_notificationService, NotificationSeverity.Error, _localizerNotification["Error"], ex.Message);
            return;
        }
    }
}
