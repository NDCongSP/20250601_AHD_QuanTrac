using Application.DTOs.Request.Account;
using Application.DTOs.Response.Account;

namespace UI.Pages.Users;

public partial class UserManager
{
    List<GetUserWithRoleResponseDTO> _users = new List<GetUserWithRoleResponseDTO>();
    List<GetUserWithRoleResponseDTO> _userSearch = new List<GetUserWithRoleResponseDTO>();
    GetUserWithRoleResponseDTO _userModel = new GetUserWithRoleResponseDTO();

    CreateAccountRequestDTO _registerModel = new CreateAccountRequestDTO();
    string _roleSelect;
    List<string> _role = new List<string>() { "User", "Operator" };

    RadzenDataGrid<GetUserWithRoleResponseDTO> _profileGrid;
    UserSearchRequestDTO _searchModel = new UserSearchRequestDTO();

    protected override async Task OnInitializedAsync()
    {
        if (true)
        {
            await base.OnInitializedAsync();
            LayoutState.SetTitle("QUẢN LÝ NGƯỜI DÙNG");
            await RefreshDataAsync();
        }
    }

    async Task DeleteItemAsync(UpdateDeleteRequestDTO model)
    {
        try
        {
            var confirm = await _dialogService.Confirm($"{_localizer["Confirmation.Delete"]} {_localizer["User"]}: {model.Name}?", $"{_localizer["Delete"]} {_localizer["User"]}", new ConfirmOptions()
            {
                OkButtonText = _localizer["Yes"],
                CancelButtonText = _localizer["No"],
                AutoFocusFirstElement = true,
            });

            if (confirm == null || confirm == false) return;

            var res = await _accountServices.DeleteUserAsync(model);

            if (res.Flag)
            {
                _notificationService.Notify(new NotificationMessage()
                {
                    Severity = NotificationSeverity.Success,
                    Summary = _localizer["Success"],
                    Detail = res.Message,
                    Duration = 5000
                });

                _registerModel = null;
                _registerModel = new CreateAccountRequestDTO();

                await RefreshDataAsync();
            }
            else
            {
                _notificationService.Notify(new NotificationMessage()
                {
                    Severity = NotificationSeverity.Error,
                    Summary = _localizer["Error"],
                    Detail = res.Message,
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

    async Task ViewItemAsync(string id)
    {
    }

    async Task EditItemAsync(string id)
    {
        _navigation.NavigateTo($"/user-detail&id={id}");
    }

    async Task AddNewItemAsync()
    {
        _navigation.NavigateTo($"/user-detail");
    }

    async Task RefreshDataAsync()
    {
        try
        {
            _role = null; _role = new List<string>();
            var resultRole = await _accountServices.GetRolesAsync();
            if (resultRole != null || resultRole?.Count > 0)
            {
                foreach (var item in resultRole)
                {
                    _role.Add(item.Name);
                }
            }

            var res = await _accountServices.GetUsersWithRolesAsync();
            _users = new List<GetUserWithRoleResponseDTO>();

            if (res != null)
                _users.AddRange(res.Where(_ => _.Roles.FirstOrDefault()?.Name != "Warehouse API"));
            _userSearch = _users;
            StateHasChanged();
        }
        catch (UnauthorizedAccessException) { }
        //catch (ApiException ex)
        //{
        //    ApiErrorResponse errorResponse = null;

        //    if (ex.Content != null)
        //    {
        //        errorResponse = JsonConvert.DeserializeObject<ApiErrorResponse>(ex.Content.ToString());
        //    }

        //    NotificationHelper.ShowNotification(_notificationService, NotificationSeverity.Error, _localizerNotification["Error"], _localizerNotification[errorResponse?.error]);
        //    return;
        //}
        //catch (Exception ex)
        //{
        //    NotificationHelper.ShowNotification(_notificationService, NotificationSeverity.Error, _localizerNotification["Error"], ex.Message);
        //    return;
        //}
    }

    async Task SubmitData(UserSearchRequestDTO arg)
    {
        _userSearch = _users.Where(x => (string.IsNullOrEmpty(arg.Keyword)
                                    || x.Email.ToLower().Contains(arg.Keyword.ToLower())
                                    || x.UserName.ToLower().Contains(arg.Keyword.ToLower())
                                    || x.FullName.ToLower().Contains(arg.Keyword.ToLower())) &&
                                    (string.IsNullOrEmpty(arg.RoleID) || x.Roles.Any(xx => xx.Name.ToLower() == arg.RoleID.ToLower()))).ToList();
        StateHasChanged();
    }
    async Task ClearFilter()
    {
        _searchModel = new UserSearchRequestDTO();
        SubmitData(_searchModel);
    }

    private async Task ReloadData()
    {
        _searchModel = new();
        await RefreshDataAsync();
    }
}
