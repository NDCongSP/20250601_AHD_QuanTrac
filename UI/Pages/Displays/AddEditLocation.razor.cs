using Domain;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Radzen;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Pages.Displays;

public partial class AddEditLocation : ComponentBase
{
    [Inject] public required IStringLocalizer<App> L { get; set; }
    [Inject] public required NavigationManager NavigationManager { get; set; }
    [Inject] public required DialogService DialogService { get; set; }
    [Inject] public required NotificationService NotificationService { get; set; }

    [Parameter] public string? Id { get; set; }

    private LocationInfoModel _location = new() { Stations = new List<StationInfoModel>() };
    private bool _isSaving;
    private bool _isEditMode => !string.IsNullOrEmpty(Id);
    private RadzenDataGrid<StationInfoModel>? _stationsGrid;
    private readonly List<StationInfoModel> _stationsToInsert = new();

    protected override async Task OnInitializedAsync()
    {
        if (_isEditMode && !string.IsNullOrEmpty(Id))
        {
            await LoadLocationAsync();
        }
    }

    private async Task LoadLocationAsync()
    {
        try
        {
            _isSaving = true;
            // TODO: Uncomment and implement actual API call
            /*
            var result = await _locationService.GetByIdAsync(Id);
            if (result != null && result.Success)
            {
                _location = result.Data;
            }
            else
            {
                NotificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = L["Error"],
                    Detail = L["Failed to load location"],
                    Duration = 4000
                });
                NavigationManager.NavigateTo("/location-management");
            }
            */
        }
        catch (Exception ex)
        {
            NotificationService.Notify(new NotificationMessage
            {
                Severity = NotificationSeverity.Error,
                Summary = L["Error"],
                Detail = ex.Message,
                Duration = 4000
            });
        }
        finally
        {
            _isSaving = false;
            StateHasChanged();
        }
    }

    private void AddStation()
    {
        _location.Stations ??= new List<StationInfoModel>();
        _location.Stations.Add(new StationInfoModel());
        StateHasChanged();
    }

    private async Task DeleteStation(StationInfoModel station)
    {

        var confirmed = await _dialogService.Confirm(
            L["Are you sure you want to delete this station?"],
            L["Confirm Delete"] ?? "Confirm Delete",
            new ConfirmOptions()
            {
                OkButtonText = L["Yes"] ?? "Yes",
                CancelButtonText = L["No"] ?? "No"
            });

        if (confirmed == true)
        {
            _location.Stations.Remove(station);
            _notificationService.Notify(new NotificationMessage
            {
                Severity = NotificationSeverity.Success,
                Summary = L["Success"] ?? "Success",
                Detail = L["Station deleted successfully"] ?? "Station deleted successfully"
            });
        }
    }

    // Station CRUD Operations
    private async Task EditRow(StationInfoModel station)
    {
        if (_stationsGrid != null)
        {
            await _stationsGrid.EditRow(station);
        }
    }

    private async Task OnUpdateRow(StationInfoModel station)
    {
        if (_location.Stations != null && _location.Stations.Count > 0 && station == _location.Stations[^1])
        {
            await SaveRow(station);
        }
    }

    private async Task SaveRow(StationInfoModel station)
    {
        if (string.IsNullOrWhiteSpace(station.Name))
        {
            NotificationService.Notify(new NotificationMessage
            {
                Severity = NotificationSeverity.Error,
                Summary = L["Error"],
                Detail = L["Station name is required"] ?? "Station name is required",
                Duration = 4000
            });
            return;
        }

        if (string.IsNullOrWhiteSpace(station.Path))
        {
            NotificationService.Notify(new NotificationMessage
            {
                Severity = NotificationSeverity.Error,
                Summary = L["Error"],
                Detail = L["Station path is required"],
                Duration = 4000
            });
            return;
        }

        if (CheckDuplicateStation(station))
        {
            NotificationService.Notify(new NotificationMessage
            {
                Severity = NotificationSeverity.Error,
                Summary = L["Error"],
                Detail = L["A station with the same name already exists"],
                Duration = 4000
            });
            return;
        }

        _location.Stations ??= new List<StationInfoModel>();

        if (station.Id == null || station.Id == 0)
        {
            // New station
            station.Id = _location.Stations.Any() ? _location.Stations.Max(s => s.Id) + 1 : 1;
            _location.Stations.Add(station);
            //_stationsGrid.De(station);
        }

        await _stationsGrid.UpdateRow(station);
        StateHasChanged();
    }

    private bool CheckDuplicateStation(StationInfoModel station)
    {
        return _location.Stations?.Any(s =>
            s.Id != station.Id &&
            string.Equals(s.Name, station.Name, StringComparison.OrdinalIgnoreCase)) == true;
    }

    private void CancelEdit(StationInfoModel station)
    {
        if (_stationsToInsert.Contains(station))
        {
            _stationsToInsert.Remove(station);
        }
        _stationsGrid?.CancelEditRow(station);
        StateHasChanged();
    }

    private async Task DeleteRow(StationInfoModel station)
    {
        var confirmed = await DialogService.Confirm(
            L["Are you sure you want to delete this station?"],
            L["Confirm Delete"] ?? "Confirm Delete",
            new ConfirmOptions
            {
                OkButtonText = L["Yes"] ?? "Yes",
                CancelButtonText = L["No"] ?? "No"
            });

        if (confirmed == true && _location.Stations != null)
        {
            if (_location.Stations.Contains(station))
            {
                _location.Stations.Remove(station);
                if (_stationsGrid != null)
                {
                    await _stationsGrid.Reload();
                }

                NotificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Success,
                    Summary = L["Success"] ?? "Success",
                    Detail = L["Station deleted successfully"] ?? "Station deleted successfully",
                    Duration = 4000
                });
            }
        }
    }

    private async Task InsertRow()
    {
        if (string.IsNullOrWhiteSpace(_location.Name?.Trim()))
        {
            NotificationService.Notify(new NotificationMessage
            {
                Severity = NotificationSeverity.Error,
                Summary = L["Error"] ?? "Error",
                Detail = L["Please save the location before adding stations"] ?? "Please save the location before adding stations",
                Duration = 4000
            });
            return;
        }

        _location.Stations ??= new List<StationInfoModel>();
        var newStation = new StationInfoModel();
        _stationsToInsert.Add(newStation);
        if (_stationsGrid != null)
        {
            await _stationsGrid.InsertRow(newStation);
        }
    }
    private async Task OnSave()
    {
        try
        {
            _isSaving = true;

            // Basic validation
            if (string.IsNullOrWhiteSpace(_location.Name?.Trim()))
            {
                NotificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = L["Validation Error"],
                    Detail = L["Location name is required"],
                    Duration = 4000
                });
                return;
            }

            // Validate stations
            if (_location.Stations != null && _location.Stations.Any())
            {
                foreach (var station in _location.Stations)
                {
                    if (string.IsNullOrWhiteSpace(station.Name) || string.IsNullOrWhiteSpace(station.Path))
                    {
                        NotificationService.Notify(new NotificationMessage
                        {
                            Severity = NotificationSeverity.Error,
                            Summary = L["Validation Error"],
                            Detail = L["All stations must have both name and path"],
                            Duration = 4000
                        });
                        return;
                    }
                }
            }

            // TODO: Uncomment and implement actual API call
            /*
            if (_isEditMode)
            {
                var updateResult = await _locationService.UpdateAsync(_location);
                if (updateResult.Success)
                {
                    NotificationService.Notify(new NotificationMessage
                    {
                        Severity = NotificationSeverity.Success,
                        Summary = L["Success"],
                        Detail = L["Location updated successfully"],
                        Duration = 4000
                    });
                    NavigationManager.NavigateTo("/location-management");
                }
                else
                {
                    NotificationService.Notify(new NotificationMessage
                    {
                        Severity = NotificationSeverity.Error,
                        Summary = L["Error"],
                        Detail = updateResult.Message ?? L["Failed to update location"],
                        Duration = 4000
                    });
                }
            }
            else
            {
                var createResult = await _locationService.CreateAsync(_location);
                if (createResult.Success)
                {
                    NotificationService.Notify(new NotificationMessage
                    {
                        Severity = NotificationSeverity.Success,
                        Summary = L["Success"],
                        Detail = L["Location created successfully"],
                        Duration = 4000
                    });
                    NavigationManager.NavigateTo("/location-management");
                }
                else
                {
                    NotificationService.Notify(new NotificationMessage
                    {
                        Severity = NotificationSeverity.Error,
                        Summary = L["Error"],
                        Detail = createResult.Message ?? L["Failed to create location"],
                        Duration = 4000
                    });
                }
            }
            */
        }
        catch (Exception ex)
        {
            NotificationService.Notify(new NotificationMessage
            {
                Severity = NotificationSeverity.Error,
                Summary = L["Error"],
                Detail = ex.Message,
                Duration = 4000
            });
        }
        finally
        {
            _isSaving = false;
            StateHasChanged();
        }
    }

        private async Task OnCancel()
        {
            if (HasUnsavedChanges())
            {
                var confirmed = await _dialogService.Confirm(
                    L["You have unsaved changes. Are you sure you want to leave?"] ?? "You have unsaved changes. Are you sure you want to leave?",
                    L["Confirm"] ?? "Confirm",
                    new ConfirmOptions()
                    {
                        OkButtonText = L["Yes"] ?? "Yes",
                        CancelButtonText = L["No"] ?? "No"
                    });

                if (confirmed != true)
                    return;
            }

            _navigation.NavigateTo("/location-management");
        }

        private bool HasUnsavedChanges()
        {
            // Check if we have any unsaved changes
            if (_isEditMode)
                return true;

            // For new locations, check if we have any data entered
            return !string.IsNullOrWhiteSpace(_location.Name) ||
                   !string.IsNullOrWhiteSpace(_location.Description) ||
                   _location.Stations?.Any(s =>
                       !string.IsNullOrWhiteSpace(s.Name) ||
                       !string.IsNullOrWhiteSpace(s.Path)) == true;
        }
    }