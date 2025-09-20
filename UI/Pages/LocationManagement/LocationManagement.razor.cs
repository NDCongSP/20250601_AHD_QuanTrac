using Domain;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using RestEase;

namespace UI.Pages.LocationManagement;

public partial class LocationManagement
{
    [Inject]
    private IStringLocalizer<LocationManagement> Localizer { get; set; } = null!;
    private List<LocationInfoModel> locations { get; set; } = new();

    [Parameter]
    public EventCallback OnCreate { get; set; }

    [Parameter]
    public EventCallback<(string Id, LocationInfoModel Model)> OnEdit { get; set; }

    [Parameter]
    public bool RefreshOnLoad { get; set; }
    private bool IsFirstLoad { get; set; } = true;
    protected override async Task OnParametersSetAsync()
    {
        if (RefreshOnLoad || IsFirstLoad)
        {
            IsFirstLoad = false;
            await RefreshDataAsync();
        }
    }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        LayoutState.SetTitle(Localizer["lbl_LocationManagement"].Value);
    }

    async Task RefreshDataAsync()
    {
        try
        {
            var result = await _ft01Service.GetAllAsync();
            if (result.Succeeded && result.Data.Any())
            {
                string stringJson = result.Data.First().C001 ?? string.Empty;
                locations = JsonConvert.DeserializeObject<List<LocationInfoModel>>(stringJson);
            }
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

            NotificationHelper.ShowNotification(_notificationService, NotificationSeverity.Error, _localizer["Error"], _localizer[errorResponse?.error]);
            return;
        }
        catch (Exception ex)
        {
            NotificationHelper.ShowNotification(_notificationService, NotificationSeverity.Error, _localizer["Error"], ex.Message);
            return;
        }
    }

    private async Task HandleCreateClick()
    {
        if (OnCreate.HasDelegate)
            await OnCreate.InvokeAsync();
    }

    private async Task HandleEditClick(string id, LocationInfoModel model)
    {
        if (OnEdit.HasDelegate)
            await OnEdit.InvokeAsync((id, model));
    }

    private async Task DeleteItem(LocationInfoModel item)
    {
        if (item == null) return;

        var confirmed = await _dialogService.Confirm(
            string.Format(Localizer["msg_DeleteConfirmation"], item.Name),
            Localizer["lbl_DeleteConfirmation"].Value,
            new ConfirmOptions()
            {
                OkButtonText = Localizer["btn_Delete"].Value,
                CancelButtonText = Localizer["Cancel"].Value
            });

        if (confirmed == true && item.Id != null)
        {
            try
            {
                var result = await _ft01Service.DeleteLocationAsync(item.Id);
                if (result.Succeeded)
                {
                    await RefreshDataAsync();
                    NotificationHelper.ShowNotification(_notificationService,
                        NotificationSeverity.Success,
                        Localizer["lbl_Success"].Value,
                        string.Format(Localizer["msg_DeleteSuccess"], item.Name));
                }
                else
                {
                    NotificationHelper.ShowNotification(_notificationService,
                        NotificationSeverity.Error,
                        Localizer["lbl_Error"].Value,
                        string.Format(Localizer["msg_DeleteError"], string.Join(',', result.Messages)));
                }
            }
            catch (Exception ex)
            {
                NotificationHelper.ShowNotification(_notificationService,
                    NotificationSeverity.Error,
                    Localizer["lbl_Error"].Value,
                    string.Format(Localizer["msg_DeleteError"], ex.Message));
            }
        }
    }
}
