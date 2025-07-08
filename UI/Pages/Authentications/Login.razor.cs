using Application.DTOs.Request.Account;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System.Security.Claims;

namespace UI.Pages.Authentications;

[AllowAnonymous]
public partial class Login
{
    LoginResponse login;
    private string token;

    LoginRequestDTO _loginRequest = new LoginRequestDTO();

    bool password = true;
    void TogglePassword()
    {
        password = !password;
    }

    bool _remember = false;

    private AuthenticationState? authState;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    }

    async void Submit(LoginRequestDTO arg)
    {
        //Console.WriteLine($"Username: {arg.EmailAddress} and password: {arg.Password}");
        try
        {
            var result = await _accountServices.LoginAccountAsync(arg);

            //Fail
            if (!result.Flag)
            {
                var error = JsonConvert.DeserializeObject<ErrorResponse>(result.Message)?.Errors.FirstOrDefault();

                NotificationHelper.ShowNotification(_notificationService
                   , error?.Key == "Warning" ? NotificationSeverity.Warning : NotificationSeverity.Error
                   , _localizer[error?.Key], _localizer[error?.Value]);

                return;
            }

            #region Check the pasword is a default pass then madatory to change pass.
            var ck = await _accountServices.CheckPasswordAsync(arg.EmailAddress, arg.Password);
            if (arg.Password == "wms@tealife.co.jp_RS1")
            {
                _navigation.NavigateTo($"/changepassdefault/{arg.EmailAddress}");

                _accountServices.LoginAccountAsync(arg);
                return;
            }
            #endregion

            token = result.Token;
            login = result;

            NotificationHelper.ShowNotification(_notificationService, NotificationSeverity.Success, _localizer["Success"], _localizer["Login OK."]);

            //await InvokeAsync(StateHasChanged);
            //StateHasChanged();

            var claimsIdentity = new ClaimsIdentity(JwtHelper.GetClaimsFromJwt(token), "jwt");
            var user = new ClaimsPrincipal(claimsIdentity);

            //_httpInterceptorManager.RegisterEvent();
            GlobalVariable.UserAuthorizationInfo.UserName = user.Identity.Name;
            GlobalVariable.UserAuthorizationInfo.FullName = user.FindFirst("FullName").Value;
            GlobalVariable.UserAuthorizationInfo.EmailName = user.FindFirst(ClaimTypes.Email).Value;
            GlobalVariable.UserAuthorizationInfo.UserId = user.FindFirst("UserId").Value;
            authState = await _authStateProvider.GetAuthenticationStateAsync();

            //truyen authState
            GlobalVariable.AuthenticationStateTask = authState;

            if (GlobalVariable.AuthenticationStateTask.HasRole(ConstantExtention.Roles.Admin))
                _navigation.NavigateTo("/userlist");
            else if (GlobalVariable.AuthenticationStateTask.HasRole(ConstantExtention.Roles.Operation))
                _navigation.NavigateTo("/");
            else
                _navigation.NavigateTo("/numbersequence");
        }
        catch (Exception ex)
        {
            _notificationService.Notify(new NotificationMessage
            {
                Severity = NotificationSeverity.Error,
                Summary = _localizer["Error"],
                Detail = $"Login fail: {ex.Message}",
                Duration = 5000
            });
        }
    }

    void Cancel()
    {

    }

    void OnRegister(string name)
    {
        Console.WriteLine($"{name} -> Register");
    }

    void OnResetPassword(string value, string name)
    {
        Console.WriteLine($"{name} -> ResetPassword for user: {value}");
    }
}
