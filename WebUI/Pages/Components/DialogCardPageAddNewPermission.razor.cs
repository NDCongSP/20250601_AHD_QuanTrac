using Microsoft.AspNetCore.Components;
using Application.DTOs.Response.Account;
using Application.DTOs.Response;
using Newtonsoft.Json;
using RestEase;
using WebUI.Models;

namespace WebUI.Pages.Components
{
    public partial class DialogCardPageAddNewPermission : DetailBaseComponent
    {
        [Parameter] public PermissionsListResponseDTO _model { get; set; } = new PermissionsListResponseDTO();

        List<GetRoleResponseDTO> _roles = new List<GetRoleResponseDTO>();
        IList<string> _selectedRoles = [];
        bool _visibleBtnSubmit = true, _disable = false;
        string _id = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            await RefreshDataAsync();
        }
        
        //protected override async Task OnParametersSetAsync()
        //{
        //    await RefreshDataAsync();
        //}

        async Task RefreshDataAsync()
        {
            try
            {
                if (!Title.Contains($"{_localizer["Detail.Create"]}"))
                {
                    _disable = true;
                }

                if (Title.Contains("|"))
                {

                    var arr = Title.Split('|');
                    Title = arr[0];
                    _id = arr[1];

                    var resultPermision = await _permissionsServices.GetByIdAsync(Guid.Parse(_id));
                    if (!resultPermision.Succeeded)
                    {
                        var error = JsonConvert.DeserializeObject<ErrorResponse>(resultPermision.Messages.FirstOrDefault())?.Errors.FirstOrDefault();

                        NotificationHelper.ShowNotification(_notificationService
                            , error?.Key == "Warning" ? NotificationSeverity.Warning : NotificationSeverity.Error
                            , _localizerNotification[error?.Key], _localizerNotification[error?.Value]);

                        return;
                    }

                    var res = resultPermision.Data;
                    _model.Id = res.Id;
                    _model.Name = res.Name;
                    _model.Description = res.Description;
                    _model.CreateAt = res.CreateAt;
                    _model.CreateOperatorId = res.CreateOperatorId;

                    var r2p = await _roleToPermissionServices.GetByPermissionsIdAsync(_model.Id.ToString());

                    if (r2p.Succeeded)
                    {
                        foreach (var item in r2p.Data)
                        {
                            _selectedRoles.Add(item.RoleId.ToString());

                            _model.AssignedToRoles.Add(new GetRoleResponseDTO()
                            {
                                Id = item.RoleId.ToString(),
                                Name = item.RoleName,
                            });
                        }
                    }
                }

                #region Get role information
                _roles = new List<GetRoleResponseDTO>();

                var result = await _authenServices.GetRolesAsync();

                foreach (var role in result)
                {
                    _roles.Add(role);
                }

                if (_roles == null)
                {
                    NotificationHelper.ShowNotification(_notificationService, NotificationSeverity.Warning, _localizerNotification["Warning"]
                        , _localizerNotification["Result user null."]);

                    return;
                }
                #endregion

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

        async void Submit(PermissionsListResponseDTO arg)
        {
            try
            {
                var confirm = await _dialogService.Confirm(_localizer["Confirmation.Create"] + _localizer["Permission.Name"] + $": {arg.Name}?", _localizer["Create"] + " " + _localizer["Permission.Name"], new ConfirmOptions()
                {
                    OkButtonText = _localizer["Yes"],
                    CancelButtonText = _localizer["No"],
                    AutoFocusFirstElement = true,
                });

                if (confirm == null || confirm == false) return;

                //If edit user then clear roles for adding new roles
                if (_model != null)
                {
                    arg.AssignedToRoles = null;
                    arg.AssignedToRoles = new List<GetRoleResponseDTO>();
                }

                foreach (var role in _selectedRoles)
                {
                    var r = _roles.FirstOrDefault(x => x.Id == role);
                    arg.AssignedToRoles.Add(new GetRoleResponseDTO() { Id = r.Id, Name = r.Name });
                }

                _model.Name = arg.Name;
                _model.Description = arg.Description;
                _model.AssignedToRoles = arg.AssignedToRoles;

                var response = await _permissionsServices.AddOrEditAsync(_model);

                if (!response.Succeeded)
                {
                    var error = JsonConvert.DeserializeObject<ErrorResponse>(response.Messages.FirstOrDefault())?.Errors.FirstOrDefault();

                    NotificationHelper.ShowNotification(_notificationService
                       , error?.Key == "Warning" ? NotificationSeverity.Warning : NotificationSeverity.Error
                       , _localizerNotification[error?.Key], _localizerNotification[error?.Value]);

                    return;
                }

                NotificationHelper.ShowNotification(_notificationService, NotificationSeverity.Success, _localizerNotification["Success"], _localizerNotification["Success"]);
                IsNeedsRefresh = true;
                _dialogService.Close("Success");
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

        async Task DeleteItemAsync(PermissionsListResponseDTO model)
        {
            try
            {
                var confirm = await _dialogService.Confirm($"{_localizer["Confirmation.Delete"]}?", $"{_localizer["Permission.Name"]}: {model.Name}", new ConfirmOptions()
                {
                    OkButtonText = _localizer["Yes"],
                    CancelButtonText = _localizer["No"],
                    AutoFocusFirstElement = true,
                });

                if (confirm == null || confirm == false) return;

                var res = await _permissionsServices.DeleteAsync(model);

                if (!res.Succeeded)
                {
                    var error = JsonConvert.DeserializeObject<ErrorResponse>(res.Messages.FirstOrDefault())?.Errors.FirstOrDefault();

                    NotificationHelper.ShowNotification(_notificationService
                       , error?.Key == "Warning" ? NotificationSeverity.Warning : NotificationSeverity.Error
                       , _localizerNotification[error?.Key], _localizerNotification[error?.Value]);

                    return;
                }


                NotificationHelper.ShowNotification(_notificationService, NotificationSeverity.Success, _localizerNotification["Success"], _localizerNotification["Success"]);
                await OnSaved.InvokeAsync(true);
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
    }
}
