using Domain;
using Domain.Enums;
using Microsoft.AspNetCore.Components;
using Microsoft.CodeAnalysis;

namespace UI.Pages.LocationManagement;

public partial class AddEditLocation : ComponentBase
{
    [Parameter]
    public EnumMode Mode { get; set; }

    [Parameter]
    public string? Id { get; set; } // Id khi Edit, có thể null khi Create

    [Parameter]
    public LocationInfoModel Model { get; set; } = new(); // Dữ liệu đang chỉnh sửa

    [Parameter]
    public EventCallback OnBack { get; set; } // Gọi khi bấm nút quay lại

    [Parameter]
    public EventCallback OnSaved { get; set; } // Gọi khi lưu thành công


    #region Data grid Detail
    private bool _isSaving;
    private RadzenDataGrid<StationInfoModel>? _stationsGrid;
    private readonly List<StationInfoModel> _stationsToInsert = new();

    // Flags to manage the state of row editing/insertion
    private bool _isInserting = false;
    private bool _isEditing = false;
    #endregion
    protected override async Task OnParametersSetAsync()
    {
        if (Mode == EnumMode.Create)
        {
            Model = new LocationInfoModel();
        }
    }

    private async Task HandleBackClick()
    {
        if (OnBack.HasDelegate)
            await OnBack.InvokeAsync();
    }

    private async Task HandleSaveClick()
    {
        try
        {
            var result = await _ft01Service.AddOrUpdateLocationAsync(Model);
            if (result.Succeeded)
            {
                await OnCancel();
                NotificationHelper.ShowNotification(_notificationService,
                    NotificationSeverity.Success,
                    "Thành công",
                    $"{(Mode == EnumMode.Create ? "Đã thêm" : "Đã cập nhật")} trạm '{Model.Name}' thành công");

                if (OnSaved.HasDelegate)
                    await OnSaved.InvokeAsync();
            }
            else
            {
                NotificationHelper.ShowNotification(_notificationService,
                    NotificationSeverity.Error,
                    "Lỗi",
                    $"{(Mode == EnumMode.Create ? "Thêm" : "Cập nhật")} trạm không thành công: {string.Join(',', result.Messages)}");
            }
        }
        catch (Exception ex)
        {
            NotificationHelper.ShowNotification(_notificationService,
                NotificationSeverity.Error,
                "Lỗi",
                $"{(Mode == EnumMode.Create ? "Thêm" : "Cập nhật")} trạm không thành công: {ex.Message}");
        }
    }

    private async Task DeleteItem(LocationInfoModel item)
    {
        if (item == null) return;

        var confirmed = await _dialogService.Confirm(
            $"Bạn có chắc chắn muốn xóa trạm '{item.Name}'?",
            "Xác nhận xóa",
            new ConfirmOptions()
            {
                OkButtonText = "Xóa",
                CancelButtonText = "Hủy"
            });

        if (confirmed == true && item.Id != null)
        {
            try
            {
                var result = await _ft01Service.DeleteLocationAsync(item.Id);
                if (result.Succeeded)
                {
                    // TODO: Update the list of locations if this page displays a list
                    NotificationHelper.ShowNotification(_notificationService,
                        NotificationSeverity.Success,
                        "Thành công",
                        $"Đã xóa trạm '{item.Name}' thành công");
                }
                else
                {
                    NotificationHelper.ShowNotification(_notificationService,
                        NotificationSeverity.Error,
                        "Lỗi",
                        $"Xóa trạm không thành công: {string.Join(',', result.Messages)}");
                }
            }
            catch (Exception ex)
            {
                NotificationHelper.ShowNotification(_notificationService,
                    NotificationSeverity.Error,
                    "Lỗi",
                    $"Xóa trạm không thành công: {ex.Message}");
            }
        }
    }

    private async Task AddStation()
    {
        var result = await _dialogService.OpenAsync<AddStationDialog>(
            "Thêm trạm",
            new Dictionary<string, object>()
            {
            },
            new DialogOptions
            {
                Width = "700px",
                Height = "auto",
                Resizable = true
            }
        );

        if (result is StationInfoModel newStation)
        {
            if (string.IsNullOrWhiteSpace(newStation.Name) || string.IsNullOrWhiteSpace(newStation.Path))
            {
                _notificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = "Lỗi",
                    Detail = "Tên và đường dẫn không được để trống",
                    Duration = 4000
                });
                return;
            }

            Model.Stations ??= new List<StationInfoModel>();
            if (CheckDuplicateStation(newStation))
            {
                _notificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = "Lỗi",
                    Detail = "Đã tồn tại trạm có tên tương tự",
                    Duration = 4000
                });
                return;
            }

            newStation.Id = Model.Stations.Any() ? (Model.Stations.Max(s => s.Id) ?? 0) + 1 : 1;
            Model.Stations.Add(newStation);
            if (_stationsGrid != null)
            {
                await _stationsGrid.Reload();
            }
        }
    }

    private async Task DeleteStation(StationInfoModel station)
    {

        var confirmed = await _dialogService.Confirm(
            "Bạn có chắc chắn muốn xóa trạm này?",
            "Xác nhận xóa",
            new ConfirmOptions()
            {
                OkButtonText = "Có",
                CancelButtonText = "Không"
            });

        if (confirmed == true)
        {
            Model.Stations.Remove(station);
            _notificationService.Notify(new NotificationMessage
            {
                Severity = NotificationSeverity.Success,
                Summary = "Thành công",
                Detail = "Đã xóa trạm thành công"
            });
        }
    }

    private async Task EditRow(StationInfoModel station)
    {
        var result = await _dialogService.OpenAsync<AddStationDialog>(
            "Chỉnh sửa trạm",
            new Dictionary<string, object>()
            {
                { nameof(AddStationDialog.Initial), station }
            },
            new DialogOptions
            {
                Width = "700px",
                Height = "auto",
                Resizable = true
            }
        );

        if (result is StationInfoModel updated)
        {
            // Normalize and validate
            updated.Id = station.Id;
            if (string.IsNullOrWhiteSpace(updated.Name) || string.IsNullOrWhiteSpace(updated.Path))
            {
                _notificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = "Lỗi",
                    Detail = "Tên và đường dẫn không được để trống",
                    Duration = 4000
                });
                return;
            }

            if (CheckDuplicateStation(updated))
            {
                _notificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = "Lỗi",
                    Detail = "Đã tồn tại trạm có tên tương tự",
                    Duration = 4000
                });
                return;
            }

            // Apply updates
            station.Name = updated.Name;
            station.Path = updated.Path;
            station.Tags = updated.Tags;

            if (_stationsGrid != null)
            {
                await _stationsGrid.Reload();
            }
        }
    }

    private async Task OnUpdateRow(StationInfoModel station)
    {
        await SaveRow(station);
    }

    private async Task SaveRow(StationInfoModel station)
    {
        if (string.IsNullOrWhiteSpace(station.Name))
        {
            _notificationService.Notify(new NotificationMessage
            {
                Severity = NotificationSeverity.Error,
                Summary = "Lỗi",
                Detail = "Tên trạm không được để trống",
                Duration = 4000
            });
            return;
        }

        if (string.IsNullOrWhiteSpace(station.Path))
        {
            _notificationService.Notify(new NotificationMessage
            {
                Severity = NotificationSeverity.Error,
                Summary = "Lỗi",
                Detail = "Đường dẫn trạm không được để trống",
                Duration = 4000
            });
            return;
        }

        if (CheckDuplicateStation(station))
        {
            _notificationService.Notify(new NotificationMessage
            {
                Severity = NotificationSeverity.Error,
                Summary = "Lỗi",
                Detail = "Đã tồn tại trạm có tên tương tự",
                Duration = 4000
            });
            return;
        }

        Model.Stations ??= new List<StationInfoModel>();
        Model.Stations = Model.Stations.Where(x => x.Id > 0).ToList();  
        if (station.Id == null || station.Id == 0)
        {
            // New station
            station.Id = Model.Stations.Any() ? Model.Stations.Max(s => s.Id) + 1 : 1;
            if (!Model.Stations.Contains(station))
            {
                Model.Stations.Add(station);
            }
        }

        if (_stationsGrid != null)
        {
            await _stationsGrid.UpdateRow(station);
            _isEditing = false;
            _isInserting = false;
        }
        StateHasChanged();
    }

    private bool CheckDuplicateStation(StationInfoModel station)
    {
        return Model.Stations?.Any(s =>
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
        _isEditing = false;
        _isInserting = false;
        StateHasChanged();
    }

    private async Task DeleteRow(StationInfoModel station)
    {
        var confirmed = await _dialogService.Confirm(
            "Bạn có chắc chắn muốn xóa trạm này?",
            "Xác nhận xóa",
            new ConfirmOptions
            {
                OkButtonText = "Có",
                CancelButtonText = "Không"
            });

        if (confirmed == true && Model.Stations != null)
        {
            if (Model.Stations.Contains(station))
            {
                Model.Stations.Remove(station);
                if (_stationsGrid != null)
                {
                    await _stationsGrid.Reload();
                    // Reset editing flag after delete
                    _isEditing = false;
                }

                _notificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Success,
                    Summary = "Thành công",
                    Detail = "Đã xóa trạm thành công",
                    Duration = 4000
                });
            }
        }
    }

    private async Task InsertRow()
    {
        if (string.IsNullOrWhiteSpace(Model.Name?.Trim()))
        {
            _notificationService.Notify(new NotificationMessage
            {
                Severity = NotificationSeverity.Error,
                Summary = "Lỗi",
                Detail = "Vui lòng lưu thông tin trạm trước khi thêm trạm con",
                Duration = 4000
            });
            return;
        }

        Model.Stations ??= new List<StationInfoModel>();
        var newStation = new StationInfoModel();
        _stationsToInsert.Add(newStation);
        // Mark as inserting
        _isInserting = true;
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
            if (string.IsNullOrWhiteSpace(Model.Name?.Trim()))
            {
                _notificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = "Lỗi xác thực",
                    Detail = "Tên địa điểm không được để trống",
                    Duration = 4000
                });
                return;
            }

            if (Model.Stations != null && Model.Stations.Any())
            {
                foreach (var station in Model.Stations)
                {
                    if (string.IsNullOrWhiteSpace(station.Name) || string.IsNullOrWhiteSpace(station.Path))
                    {
                        _notificationService.Notify(new NotificationMessage
                        {
                            Severity = NotificationSeverity.Error,
                            Summary = "Lỗi xác thực",
                            Detail = "Tất cả các trạm phải có cả tên và đường dẫn",
                            Duration = 4000
                        });
                        return;
                    }
                }
            }

            var updateResult = await _ft01Service.AddOrUpdateLocationAsync(Model);
            if (updateResult.Succeeded)
            {
                _notificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Success,
                    Summary = "Thành công",
                    Detail = "Cập nhật địa điểm thành công",
                    Duration = 4000
                });
            }
            else
            {
                _notificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = "Lỗi",
                    Detail = updateResult.Messages.Any() ? string.Join(',', updateResult.Messages) : "Cập nhật địa điểm thất bại",
                    Duration = 4000
                });
            }

            if (OnSaved.HasDelegate)
            {
                await OnSaved.InvokeAsync();
            }
        }
        catch (Exception ex)
        {
            _notificationService.Notify(new NotificationMessage
            {
                Severity = NotificationSeverity.Error,
                Summary = "Lỗi",
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
        if (false)
        {
            var confirmed = await _dialogService.Confirm(
                "Bạn có thay đổi chưa được lưu. Bạn có chắc chắn muốn rời khỏi?",
                "Xác nhận",
                new ConfirmOptions()
                {
                    OkButtonText = "Có",
                    CancelButtonText = "Không"
                });

            if (confirmed != true)
                return;
        }

        await OnBack.InvokeAsync();
    }

    private bool HasUnsavedChanges()
    {
        // Check if we have any unsaved changes
        if (Mode == EnumMode.Edit && string.IsNullOrEmpty(Id) == false)
            return true; // Assume there are unsaved changes when in edit mode and an Id exists

        // For new locations, check if we have any data entered
        return !string.IsNullOrWhiteSpace(Model.Name) ||
               !string.IsNullOrWhiteSpace(Model.Description) ||
               Model.Stations?.Any(s =>
                   !string.IsNullOrWhiteSpace(s.Name) ||
                   !string.IsNullOrWhiteSpace(s.Path)) == true;
    }
}