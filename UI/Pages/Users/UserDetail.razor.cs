using Application.DTOs.Request.Account;
using Domain;
using Domain.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using RestEase;

namespace UI.Pages.Users;

public partial class UserDetail
{
    string? Title { get; set; }
    [Parameter] public string Id { get; set; } = string.Empty;
    CreateAccountRequestDTO _model = new CreateAccountRequestDTO();
    List<CreateRoleRequestDTO> _roles = new List<CreateRoleRequestDTO>();
    IList<string> _selectedRoles = [];
    List<string> _status = new List<string>();
    EnumStatus? _selectStatus;
    RadzenDataGrid<TenantAuth> _profileGrid;
    bool allowRowSelectOnRowClick = true;
    IEnumerable<int> _pageSizeOptions = new int[] { 5, 10, 20, 50, 100 };
    bool _showPagerSummary = true;
    string _pagingSummaryFormat = "Displaying page {0} of {1} <b>(total {2} records)</b>";

    bool password = true;
    void TogglePassword()
    {
        password = !password;
    }
    bool _visible = true, _disable = false;
    bool _visibleResetBtn = false;

    private string inputText = string.Empty;
    private string qrCodeBase64 = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        LayoutState.SetTitle("CHI TIẾT NGƯỜI DÙNG");
        await RefreshDataAsync();
    }

    async Task RefreshDataAsync()
    {
        try
        {
            if (Title.Contains(_localizer["Detail.Create"]))//Add
                _visibleResetBtn = false;
            else
                _visibleResetBtn = true;

            var result = await _accountServices.GetRolesAsync();

            foreach (var role in result)
            {
                _roles.Add(new CreateRoleRequestDTO() { Name = role.Name, Id = role.Id });
            }

            _selectStatus = EnumStatus.Activated;

            if (Title.Contains("|"))
            {
                var sub = Title.Split('|');
                Title = sub[0];
                Id = sub[1];
                _visible = sub[2] == "True" ? true : false;

                if (!_visible) _disable = true;//disable fielfSet user information when page opening from userInfo menu

                if (string.IsNullOrEmpty(Id))
                {
                    var user = await _accountServices.UserGetByEmailAsync(GlobalVariable.UserAuthorizationInfo.EmailName);
                    if (user != null) { Id = user.Id; }
                }

                #region Get user info
                var resultUser = await _accountServices.UserGetByIdAsync(Id);
                if (resultUser == null)
                {
                    NotificationHelper.ShowNotification(_notificationService, NotificationSeverity.Warning, _localizer["Warning"], _localizer["Result user null."]);

                    return;
                }

                _model.Email = resultUser.Email;
                _model.UserName = resultUser.UserName;
                _model.FullName = resultUser.FullName;
                _model.Status = resultUser.Status;

                _selectStatus = _model.Status;

                foreach (var role in resultUser.Roles)
                {
                    _model.Roles.Add(new CreateRoleRequestDTO()
                    {
                        Id = role.Id,
                        Name = role.Name,
                    });

                    _selectedRoles.Add(role.Id);
                }
                #endregion

                _visibleResetBtn = _model.Roles.FirstOrDefault(x => x.Name == "Warehouse Admin") != null ? false : true;

            }
            //Title = Title.Contains("View") ? $"{_localizer["Detail.View"]} User" : Title.Contains("Edit") ? $"{_localizer["Detail.Edit"]} User" : $"{_localizer["Detail.Create"]} User";

            StateHasChanged();
        }
        catch (UnauthorizedAccessException) { }
        catch (Exception ex)
        {
            NotificationHelper.ShowNotification(_notificationService, NotificationSeverity.Error, _localizer["Error"], ex.Message);
            return;
        }
    }

    async void Submit(CreateAccountRequestDTO arg)
    {
        try
        {
            if (Title.Contains(_localizer["Detail.Create"]))//Add
            {
                var confirm = await _dialogService.Confirm($"{_localizer["User"]}: {arg.UserName} {_localizer["Confirmation.Create"]}?", $"{_localizer["Create"]} {_localizer["User"]}", new ConfirmOptions()
                {
                    OkButtonText = _localizer["Yes"],
                    CancelButtonText = _localizer["No"],
                    AutoFocusFirstElement = true,
                });
                if (confirm == null || confirm == false) return;
            }
            else
            {
                var confirm = await _dialogService.Confirm($"{_localizer["User"]}: {arg.UserName} {_localizer["Confirmation.Update"]}?", $"{_localizer["UpdateUsetTitle"]}", new ConfirmOptions()
                {
                    OkButtonText = _localizer["Yes"],
                    CancelButtonText = _localizer["No"],
                    AutoFocusFirstElement = true,
                });
                if (confirm == null || confirm == false) return;
            }


            //If edit user then clear roles for adding new roles
            if (!string.IsNullOrEmpty(Id))
            {
                arg.Roles = null;
                arg.Roles = new List<CreateRoleRequestDTO>();

                //get danh sach tenant duoc dang ky cho user
                //var resU2T = await _userToTenantServices.GetByUserIdAsync(_id);
            }

            //lay dang sachs role moi
            foreach (var role in _selectedRoles.ToList())
            {
                var r = _roles.FirstOrDefault(x => x.Id == role);
                arg.Roles.Add(new CreateRoleRequestDTO() { Id = r.Id, Name = r.Name });
            }

            arg.Status = _selectStatus;
            arg.ConfirmPassword = arg.Password;
            if (Title.Contains(_localizer["Detail.Create"]))//Add
            {
                var res = await _accountServices.CreateAccountAsync(arg);

                if (!res.Flag)
                {
                    var error = JsonConvert.DeserializeObject<ErrorResponse>(res.Message)?.Errors.FirstOrDefault();

                    NotificationHelper.ShowNotification(_notificationService, NotificationSeverity.Error, _localizer[error?.Key], _localizer[error?.Value]);

                    return;
                }
                NotificationHelper.ShowNotification(_notificationService, NotificationSeverity.Success, _localizer["Success"], _localizer["Success"]);
            }
            else if (Title.Contains(_localizer["Detail.Edit"]))//update
            {
                var userInfoUpdate = new UpdateUserInfoRequestDTO();
                userInfoUpdate.Id = Id;
                userInfoUpdate.UserName = arg.UserName;
                userInfoUpdate.Email = arg.Email;
                userInfoUpdate.FullName = arg.FullName;
                userInfoUpdate.Status = _selectStatus;
                userInfoUpdate.Roles = arg.Roles;

                var res = await _accountServices.UpdateUserInfoAsync(userInfoUpdate);

                if (!res.Flag)
                {
                    var error = JsonConvert.DeserializeObject<ErrorResponse>(res.Message)?.Errors.FirstOrDefault();

                    NotificationHelper.ShowNotification(_notificationService
                        , error?.Key == "Warning" ? NotificationSeverity.Warning : NotificationSeverity.Error
                        , _localizer[error?.Key], _localizer[error?.Value]);

                    return;
                }

                NotificationHelper.ShowNotification(_notificationService, NotificationSeverity.Success, _localizer["Success"], _localizer["Success"]);
            }

        }
        catch (Exception ex)
        {
            NotificationHelper.ShowNotification(_notificationService, NotificationSeverity.Error, _localizer["Error"], ex.Message);
            return;
        }
    }

    async Task ResetPass()
    {
        var confirm = await _dialogService.Confirm(_localizer["Do you want to reset password?"]
               , _localizer["Reset Password"]
               , new ConfirmOptions()
               {
                   OkButtonText = _localizer["Yes"],
                   CancelButtonText = _localizer["No"],
                   AutoFocusFirstElement = true,
               });
        Console.WriteLine($"Confirm: {confirm}");
        if (confirm == null || confirm == false) return;

        var response = await _accountServices.ChangePassAsync(new ChangePassRequestDTO()
        {
            Id = Id,
            NewPassword = "wms@tealife.co.jp_RS1",
            ConfirmNewPassword = "wms@tealife.co.jp_RS1"
        });

        if (!response.Flag)
        {
            _notificationService.Notify(new NotificationMessage()
            {
                Severity = NotificationSeverity.Error,
                Summary = _localizer["Error"],
                Detail = response.Message,
                Duration = 5000
            });

            return;
        }

        NotificationHelper.ShowNotification(_notificationService, NotificationSeverity.Success, _localizer["Success"], _localizer["Success"]);
    }


    async void RefreshData()
    {
        try
        {
            //_ovenId = int.TryParse(OvenId, out int value) ? value : 0;

            //var res = await _ft01Client.GetAllAsync();

            //if (res == null)
            //    return;
            //_ft01 = res.Data.ToList();

            //_ovenInfo = JsonConvert.DeserializeObject<OvensInfo>(_ft01.FirstOrDefault().C001).FirstOrDefault(x => x.Id == _ovenId);
        }
        catch (Exception ex)
        {
            _notificationService.Notify(new NotificationMessage
            {
                Severity = NotificationSeverity.Error,
                Summary = _localizer["Error"],
                Detail = ex.Message,
                Duration = 5000
            });
            return;
        }

        StateHasChanged();
    }
    
    async Task DisableUser()
    {
        _notificationService.Notify(new NotificationMessage()
        {
            Severity = NotificationSeverity.Info,
            Summary = "Info",
            Detail = "Disable click",
            Duration = 1000
        });
    }

    void ShowTooltip(ElementReference elementReference, TooltipOptions options = null)
    {
        _tooltipService.Open(elementReference, "Full name", options);
    }

    // Method to print the QR code
    private async Task PrintQRCode()
    {
        await _jsRuntime.InvokeVoidAsync("printQRCode");
    }

    async Task DeleteItemAsync(CreateAccountRequestDTO model)
    {
        try
        {
            var d = new UpdateDeleteRequestDTO()
            {
                Id = Id,
                Name = model.UserName
            };

            var confirm = await _dialogService.Confirm($"{_localizer["Confirmation.Delete"]}?", $"{_localizer["User"]}: {d.Name}", new ConfirmOptions()
            {
                OkButtonText = _localizer["Yes"],
                CancelButtonText = _localizer["No"],
                AutoFocusFirstElement = true,
            });

            if (confirm == null || confirm == false) return;

            var res = await _accountServices.DeleteUserAsync(d);

            if (!res.Flag)
            {
                var error = JsonConvert.DeserializeObject<ErrorResponse>(res.Message)?.Errors.FirstOrDefault();

                NotificationHelper.ShowNotification(_notificationService
                    , error?.Key == "Warning" ? NotificationSeverity.Warning : NotificationSeverity.Error
                    , _localizer[error?.Key], _localizer[error?.Value]);

                return;
            }

            NotificationHelper.ShowNotification(_notificationService, NotificationSeverity.Success, _localizer["Success"], _localizer["Success"]);
        }
        catch (Exception ex)
        {
            NotificationHelper.ShowNotification(_notificationService, NotificationSeverity.Error, _localizer["Error"], ex.Message);
            return;
        }
    }
}
