using Application.DTOs.Request.Account;
using Application.DTOs.Response.Account;
using AutoMapper;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace UI.Pages.Roles;

public partial class RoleDetail
{
    string Title { get; set; } = "";
    [Parameter] public string Id { get; set; }
    [Parameter] public CreateRoleRequestDTO _model { get; set; } = new CreateRoleRequestDTO();
    private GetRoleResponseDTO _roleInfo;

    List<PermissionsInRoleModel> _dataGrid = new List<PermissionsInRoleModel>();
    RadzenDataGrid<PermissionsInRoleModel> _profileGrid;
    IEnumerable<int> _pageSizeOptions = new int[] { 5, 10, 20, 30, 100, 200 };
    bool allowRowSelectOnRowClick = false;
    IList<PermissionsInRoleModel> _gridSelected = [];

    bool _visibleBtnSubmit = true;
    bool _disable = false;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
            LayoutState.SetTitle(_localizer["RoleDetail.Title"]);
        await RefreshDataAsync();
    }
    
    async Task RefreshDataAsync()
    {
        try
        {
            if (string.IsNullOrEmpty(Id))
            {
                _disable = true;
                Title = _localizer["Detail.Create"];
            }
            else
            {
                Title = _localizer["Detail.Edit"];
            }
            LayoutState.SetTitle($"{_localizer["RoleDetail.Title"]} - {Title}");

            #region Loading all permissions
            var p = await _permissionsServices.GetAllAsync();
            if (!p.Succeeded)
            {
                var error = JsonConvert.DeserializeObject<ErrorResponse>(p.Messages.FirstOrDefault())?.Errors.FirstOrDefault();

                NotificationHelper.ShowNotification(_notificationService
                    , error?.Key == "Warning" ? NotificationSeverity.Warning : NotificationSeverity.Error
                    , _localizer[error?.Key], _localizer[error?.Value]);

                return;
            }
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Domain.Entities.Permissions, PermissionsInRoleModel>();
            });
            var mapper = config.CreateMapper();

            _dataGrid = mapper.Map<List<PermissionsInRoleModel>>(p.Data);
            #endregion

            if (!string.IsNullOrEmpty(Id))
            {
                var roleResult = await _accountServices.RoleGetById(Id);

                if (roleResult == null)
                {
                    var error = JsonConvert.DeserializeObject<ErrorResponse>(p.Messages.FirstOrDefault())?.Errors.FirstOrDefault();

                    NotificationHelper.ShowNotification(_notificationService
                        , error?.Key == "Warning" ? NotificationSeverity.Warning : NotificationSeverity.Error
                        , _localizer[error?.Key], _localizer["Could not load data."]);

                    return;
                }

                _model.Id = roleResult.Id;
                _model.Name = roleResult.Name;
                _model.Permissions = roleResult.Permissions;

                foreach (var permission in _model.Permissions)
                {
                    var existP = _dataGrid.FirstOrDefault(_ => _.Id == permission.Id);
                    if (existP != null) existP.IsSelected = true;
                }
            }

            StateHasChanged();
        }
        catch (UnauthorizedAccessException) { }
        catch (Exception ex)
        {
            NotificationHelper.ShowNotification(_notificationService, NotificationSeverity.Error, _localizer["Error"], ex.Message);
            return;
        }
    }

    async void Submit(CreateRoleRequestDTO arg)
    {
        try
        {
            var response = new GeneralResponse();

            #region Get permission
            foreach (var item in _dataGrid)
            {
                var p = _model.Permissions.FirstOrDefault(_ => _.Id == item.Id);

                if (item.IsSelected)
                {
                    if (p is null)
                    {
                        _model.Permissions.Add(item);
                    }
                }
                else
                {
                    if (p is not null)
                        _model.Permissions.Remove(p);
                }

            }
            #endregion

            if (!string.IsNullOrEmpty(Id))
            {
                var confirm = await _dialogService.Confirm(_localizer["Confirmation.Update"] + _localizer["Role"] + $": {arg.Name} ", _localizer["Update"] + " " + _localizer["Role"], new ConfirmOptions()
                {
                    OkButtonText = _localizer["Yes"],
                    CancelButtonText = _localizer["No"],
                    AutoFocusFirstElement = true,
                });

                if (confirm == null || confirm == false) return;

                _model.Name = arg.Name;

                response = await _accountServices.UpdateRoleDTOAsync(_model);
            }
            else
            {
                var confirm = await _dialogService.Confirm(_localizer["Confirmation.Create"] + _localizer["Role"] + $": {arg.Name}?", _localizer["Create"] + " " + _localizer["Role"], new ConfirmOptions()
                {
                    OkButtonText = _localizer["Yes"],
                    CancelButtonText = _localizer["No"],
                    AutoFocusFirstElement = true,
                });

                if (confirm == null || confirm == false) return;

                _model.Name = arg.Name;

                response = await _accountServices.CreateRoleAsync(_model);
            }

            if (!response.Flag)
            {
                var error = JsonConvert.DeserializeObject<ErrorResponse>(response.Message)?.Errors.FirstOrDefault();

                NotificationHelper.ShowNotification(_notificationService
                    , error?.Key == "Warning" ? NotificationSeverity.Warning : NotificationSeverity.Error
                    , _localizer[error?.Key], _localizer["Could not load data."]);

                return;
            }
            NotificationHelper.ShowNotification(_notificationService, NotificationSeverity.Success, _localizer["Success"], _localizer["Success"]);

            _dialogService.Close("Success");
        }
        catch (Exception ex)
        {
            NotificationHelper.ShowNotification(_notificationService, NotificationSeverity.Error, _localizer["Error"], ex.Message);
            return;
        }
    }

    async Task DeleteItemAsync(CreateRoleRequestDTO model)
    {
        try
        {
            var confirm = await _dialogService.Confirm(_localizer["Confirmation.Delete"] + _localizer["Role"] + $": {model.Name}?", _localizer["Delete"] + " " + _localizer["Role"], new ConfirmOptions()
            {
                OkButtonText = _localizer["Yes"],
                CancelButtonText = _localizer["No"],
                AutoFocusFirstElement = true,
            });

            if (confirm == null || confirm == false) return;

            var res = await _accountServices.DeleteRoleAsync(new UpdateDeleteRequestDTO() { Id = model.Id, Name = model.Name });

            if (!res.Flag)
            {
                var error = JsonConvert.DeserializeObject<ErrorResponse>(res.Message)?.Errors.FirstOrDefault();

                NotificationHelper.ShowNotification(_notificationService
                    , error?.Key == "Warning" ? NotificationSeverity.Warning : NotificationSeverity.Error
                    , _localizer[error?.Key], _localizer["Could not load data."]);

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
