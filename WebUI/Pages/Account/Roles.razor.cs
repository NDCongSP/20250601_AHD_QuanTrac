using Application.DTOs.Request.Account;
using Application.DTOs.Response;
using Application.DTOs.Response.Account;
using Newtonsoft.Json;
using RestEase;
using WebUI.Models;

namespace UI.Pages.Account;

public partial class Roles : ListBaseComponent<GetRoleResponseDTO>
{
    List<GetRoleResponseDTO> _dataGrid = null;
    RadzenDataGrid<GetRoleResponseDTO> _profileGrid;

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

    async Task EditItemAsync(string id)
    {
        await OnEdit.InvokeAsync(new DetailViewData($"{_localizer["Detail.Edit"]} {_localizer["Role"]}|{id}", id));
    }

    async Task AddNewItemAsync()
    {
        await OnAddNew.InvokeAsync(new DetailViewData($"{_localizer["Detail.Create"]} {_localizer["Role"]}"));
    }

    async Task ViewItemAsync(string id)
    {

        await OnView.InvokeAsync(new DetailViewData($"{_localizer["Detail.View"]} {_localizer["Role"]}|{id}", id));
    }

    async Task RefreshDataAsync()
    {
        try
        {
            var res = await _authenServices.GetRolesAsync();

            if (res == null)
            {
                _notificationService.Notify(new NotificationMessage()
                {
                    Severity = NotificationSeverity.Error,
                    Summary = _localizer["Error"],
                    Detail = _localizerNotification["Something went wrong."],
                });
                return;
            }

            
            _dataGrid = null;
            _dataGrid = new List<GetRoleResponseDTO>();
            _dataGrid = res;

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
