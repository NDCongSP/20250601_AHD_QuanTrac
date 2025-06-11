using Application.DTOs.Request.Account;
using Domain;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using RestEase;

namespace UI.Pages.Users;

public partial class UserDetail
{
    string? Title { get; set; }
    string _id = string.Empty;
    CreateAccountRequestDTO _model = new CreateAccountRequestDTO();

    List<CreateRoleRequestDTO> _roles = new List<CreateRoleRequestDTO>();
    IList<string> _selectedRoles = [];

    List<string> _status = new List<string>();
    EnumStatus? _selectStatus;

    List<CompanyTenant> _tenantList = [];
    IList<CompanyTenant> _selectedTenantList = [];
    IList<string> _selectedTenant = [];

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

    List<UserToTenant> tenantCurrent = new List<UserToTenant>();//danh sach cac tenant duoc dang ky cho user
    List<UserToTenant> tenantNew = new List<UserToTenant>();//danh sach cac tenant duoc dang ky cho user

    private string inputText = string.Empty;
    private string qrCodeBase64 = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

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

            //get tenant
            var resultTenant = await _companiesServices.GetAllAsync();
            if (resultTenant.Succeeded)
                _tenantList = resultTenant.Data;

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
                _id = sub[1];
                _visible = sub[2] == "True" ? true : false;

                if (!_visible) _disable = true;//disable fielfSet user information when page opening from userInfo menu

                if (string.IsNullOrEmpty(_id))
                {
                    var user = await _accountServices.UserGetByEmailAsync(GlobalVariable.UserAuthorizationInfo.EmailName);
                    if (user != null) { _id = user.Id; }
                }

                #region Get user info
                var resultUser = await _accountServices.UserGetById(_id);
                if (resultUser == null)
                {
                    NotificationHelper.ShowNotification(_notificationService, NotificationSeverity.Warning, _localizerNotification["Warning"], _localizerNotification["Result user null."]);

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

                var resultU2T = await _userToTenantServices.GetByUserIdAsync(resultUser.Id);

                if (!resultU2T.Succeeded)
                {
                    var error = JsonConvert.DeserializeObject<ErrorResponse>(resultU2T.Messages.FirstOrDefault())?.Errors.FirstOrDefault();

                    NotificationHelper.ShowNotification(_notificationService
                        , error?.Key == "Warning" ? NotificationSeverity.Warning : NotificationSeverity.Error
                        , _localizerNotification[error?.Key], _localizerNotification[error?.Value]);

                    return;
                }

                tenantCurrent = resultU2T.Data;

                foreach (var item in tenantCurrent)
                {
                    var r = _tenantList.FirstOrDefault(x => x.AuthPTenantId == item.TenantId);
                    _selectedTenantList.Add(r);
                }
                #endregion

                _visibleResetBtn = _model.Roles.FirstOrDefault(x => x.Name == "Warehouse Admin") != null ? false : true;

            }
            //Title = Title.Contains("View") ? $"{_localizer["Detail.View"]} User" : Title.Contains("Edit") ? $"{_localizer["Detail.Edit"]} User" : $"{_localizer["Detail.Create"]} User";

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
            if (!string.IsNullOrEmpty(_id))
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

            tenantNew = new List<UserToTenant>();
            //lay danh sach tenant moi dc chon
            foreach (var item in _selectedTenantList)
            {
                var r = _tenantList.FirstOrDefault(x => x.AuthPTenantId == item.AuthPTenantId);
                tenantNew.Add(new UserToTenant()
                {
                    Id = Guid.NewGuid(),
                    UserId = _id,
                    TenantId = item.AuthPTenantId
                });
            }

            if (Title.Contains(_localizer["Detail.Create"]))//Add
            {
                var res = await _accountServices.CreateAccountAsync(arg);

                if (!res.Flag)
                {
                    var error = JsonConvert.DeserializeObject<ErrorResponse>(res.Message)?.Errors.FirstOrDefault();

                    NotificationHelper.ShowNotification(_notificationService, NotificationSeverity.Error, _localizerNotification[error?.Key], _localizerNotification[error?.Value]);

                    return;
                }

                //var user = await _accountServices.UserGetByEmailAsync(arg.Email);

                foreach (var item in tenantNew)
                {
                    item.UserId = res.Message;
                }

                //var resU2TD = await _userToTenantServices.DeleteRangeAsync(tenantCurrent);
                var resU2T = await _userToTenantServices.AddRangeAsync(tenantNew);
                if (!resU2T.Succeeded)
                {
                    var error = JsonConvert.DeserializeObject<ErrorResponse>(resU2T.Messages.FirstOrDefault())?.Errors.FirstOrDefault();

                    NotificationHelper.ShowNotification(_notificationService
                        , error?.Key == "Warning" ? NotificationSeverity.Warning : NotificationSeverity.Error
                        , _localizerNotification[error?.Key], _localizerNotification[error?.Value]);

                    return;
                }

                NotificationHelper.ShowNotification(_notificationService, NotificationSeverity.Success, _localizerNotification["Success"], _localizerNotification["Success"]);
            }
            else if (Title.Contains(_localizer["Detail.Edit"]))//update
            {
                var userInfoUpdate = new UpdateUserInfoRequestDTO();
                userInfoUpdate.Id = _id;
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
                        , _localizerNotification[error?.Key], _localizerNotification[error?.Value]);

                    return;
                }

                //var resU2TD = await _userToTenantServices.DeleteRangeAsync(tenantCurrent);
                var resU2T = await _userToTenantServices.AddRangeAsync(tenantNew);

                if (!resU2T.Succeeded)
                {
                    var error = JsonConvert.DeserializeObject<ErrorResponse>(resU2T.Messages.FirstOrDefault())?.Errors.FirstOrDefault();

                    NotificationHelper.ShowNotification(_notificationService
                        , error?.Key == "Warning" ? NotificationSeverity.Warning : NotificationSeverity.Error
                        , _localizerNotification[error?.Key], _localizerNotification[error?.Value]);

                    return;
                }

                NotificationHelper.ShowNotification(_notificationService, NotificationSeverity.Success, _localizerNotification["Success"], _localizerNotification["Success"]);
            }

            IsNeedsRefresh = true;
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
            Id = _id,
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

        NotificationHelper.ShowNotification(_notificationService, NotificationSeverity.Success, _localizerNotification["Success"], _localizerNotification["Success"]);
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

    async Task PrintLable()
    {
        try
        {
            var dataPrint = await _accountServices.GetLabelByIdAsync(_id);

            // Lưu labelsToPrint vào LocalStorage
            await _localStorage.SetItemAsync("labelDataTransfer", dataPrint);

            // Gọi phương thức JavaScript để mở trang trong tab mới
            await _jsRuntime.InvokeVoidAsync("openTab", "/PrintUserLabel");
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

    async Task PrintLable1()
    {
        //var dataPrint = await _accountServices.GetReportBase64(_id);
        var response = await _packingListServices.GetPdfAsBase64Async("C:\\20240904_ShueiCongData\\sdsdsds.pdf");

        if (!response.Succeeded)
        {
            _notificationService.Notify(new NotificationMessage()
            {
                Severity = NotificationSeverity.Error,
                Summary = _localizer["Error"],
                Detail = response.Messages.FirstOrDefault(),
                Duration = 5000
            });

            return;
        }

        var dataPrint = response.Data;

        //var res = await _dialogService.OpenAsync<ReportViewer>($"Print label for use",
        //      new Dictionary<string, object>() { { "_pdfBase64", dataPrint } },
        //      new DialogOptions()
        //      {
        //          Width = "1000px",
        //          Height = "1000px",
        //          Resizable = true,
        //          Draggable = true,
        //          ShowClose = false,
        //          CloseDialogOnOverlayClick = true
        //      });
        await _localStorage.SetItemAsync("PackingSlip", dataPrint);
        await _jsRuntime.InvokeVoidAsync("openTab", "/PackingSlip");
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

    // Method to generate QR code
    private void GenerateQRCode()
    {
        inputText = "NGUYEN DINH CONG|COng123@456";
        if (string.IsNullOrEmpty(inputText))
            return;

        using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
        {
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(inputText, QRCodeGenerator.ECCLevel.Q);
            PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
            byte[] qrCodeImage = qrCode.GetGraphic(20);

            qrCodeBase64 = $"data:image/png;base64,{Convert.ToBase64String(qrCodeImage)}";
        }
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
                Id = _id,
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
