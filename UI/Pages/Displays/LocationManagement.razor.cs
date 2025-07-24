using Domain;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Radzen;
using RestEase;

namespace UI.Pages.Displays;

public partial class LocationManagement
{
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private DialogService DialogService { get; set; } = null!;
    private List<LocationInfoModel> Locations { get; set; } = new();
    protected override async Task OnInitializedAsync()
    {
        if (true)
        {
            await base.OnInitializedAsync();
            LayoutState.SetTitle("QUẢN LÝ TRẠM");
            await RefreshDataAsync();
        }
    }
    async Task RefreshDataAsync()
    {
        try
        {
            var result = await _ft01Service.GetAllAsync();
            if (result.Succeeded && result.Data.Any())
            {
                string stringJson = result.Data.First().C001 ?? string.Empty;
                Locations = JsonConvert.DeserializeObject<List<LocationInfoModel>>(stringJson);
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

    private void AddNewItem()
    {
        NavigationManager.NavigateTo("/add-edit-location");
    }
    
    private void ViewItem(LocationInfoModel item)
    {
        if (item?.Id != null)
        {
            NavigationManager.NavigateTo($"add-edit-location/{item.Id}");
        }
    }
    
    private async Task DeleteItem(LocationInfoModel item)
    {
        if (item == null) return;

        var confirmed = await DialogService.Confirm(
            $"Are you sure you want to delete location '{item.Name}'?",
            "Confirm Delete",
            new ConfirmOptions() 
            { 
                OkButtonText = "Delete",
                CancelButtonText = "Cancel"
            });

        if (confirmed == true && item.Id != null)
        {
            try
            {
                // TODO: Uncomment and implement actual API call
                // await _locationService.DeleteAsync(item.Id.Value);
                await RefreshDataAsync();
                NotificationHelper.ShowNotification(_notificationService, 
                    NotificationSeverity.Success, 
                    "Success", 
                    $"Location '{item.Name}' deleted successfully");
            }
            catch (Exception ex)
            {
                NotificationHelper.ShowNotification(_notificationService, 
                    NotificationSeverity.Error, 
                    "Error", 
                    $"Failed to delete location: {ex.Message}");
            }
        }
    }
}
