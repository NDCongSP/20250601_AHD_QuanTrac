using Application.DTOs.Response;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using RestEase;

namespace WebUI.Pages.Components
{
    public partial class DialogCardPageAddNewBin
    {
        [Parameter] public BinDto _model { get; set; } = new BinDto();
        [Parameter] public bool VisibleBtnSubmit { get; set; } = true;

        bool _visibleBtnSubmit = true;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            if (_model.Id == Guid.Empty)
                _visibleBtnSubmit = false;
            await RefreshDataAsync();

            StateHasChanged();
        }

        async Task RefreshDataAsync()
        {
            try
            {
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

        async void Submit(BinDto arg)
        {
            try
            {
                if (_model.Id == Guid.Empty)
                {
                    var confirm = await _dialogService.Confirm($"{_localizer["Create"]} {_localizer["Bin"]}: {arg.BinCode}?", $"{_localizer["Create"]} {_localizer["Bin"]}", new ConfirmOptions()
                    {
                        OkButtonText = _localizer["Yes"],
                        CancelButtonText = _localizer["No"],
                        AutoFocusFirstElement = true,
                    });

                    if (confirm == null || confirm == false) return;
                }
                else
                {
                    var confirm = await _dialogService.Confirm($"{_localizer["Update"]} {_localizer["Bin"]}: {arg.BinCode}?", $"{_localizer["Update"]} {_localizer["Bin"]}", new ConfirmOptions()
                    {
                        OkButtonText = _localizer["Yes"],
                        CancelButtonText = _localizer["No"],
                        AutoFocusFirstElement = true,
                    });

                    if (confirm == null || confirm == false) return;
                }

                if (_model.Id == Guid.Empty) _model.Id = Guid.NewGuid();


                _dialogService.Close(_model);
            }
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

        async Task PrintLable()
        {
            _notificationService.Notify(new NotificationMessage()
            {
                Severity = NotificationSeverity.Info,
                Summary = "Info",
                Detail = "Print label click",
                Duration = 1000
            });
        }

        async Task AddBin()
        {
            _notificationService.Notify(new NotificationMessage()
            {
                Severity = NotificationSeverity.Info,
                Summary = "Info",
                Detail = "Add bin click",
                Duration = 1000
            });
        }

        async Task DeleteItemAsync()
        {
            try
            {
                var confirm = await _dialogService.Confirm($"{_localizer["Confirmation.Delete"]} {_localizer["Bin"]}: {_model.BinCode}?", $"{_localizer["Delete"]} {_localizer["Bin"]}", new ConfirmOptions()
                {
                    OkButtonText = _localizer["Yes"],
                    CancelButtonText = _localizer["No"],
                    AutoFocusFirstElement = true,
                });

                if (confirm == null || confirm == false) return;

                _model.IsDelete = true;
                _dialogService.Close(_model);
            }
            catch (Exception ex)
            {
                NotificationHelper.ShowNotification(_notificationService
                  , NotificationSeverity.Error
                  , _localizerNotification["Error"], ex.Message);

                return;
            }
        }
    }
}
