using Microsoft.AspNetCore.Components;
using Radzen;
using Toolbelt.Blazor;

namespace Application.Services.Authen.UI;

public class HttpInterceptorManager : IHttpInterceptorManager
{
    private readonly NotificationService _notify;
    private readonly NavigationManager _navigationManager;
    private readonly HttpClientInterceptor _httpInterceptor;
    private readonly IAccount _accountService;

    public HttpInterceptorManager(NotificationService notify, NavigationManager navigationManager
        , HttpClientInterceptor httpInterceptor, IAccount accountService)
    {
        _notify = notify;
        _navigationManager = navigationManager;
        _httpInterceptor = httpInterceptor;
        _accountService = accountService;
    }

    public void DisposeEvent()
    {
        _httpInterceptor.BeforeSendAsync -= InterceptBeforeHttpAsync;
    }

    public async Task InterceptBeforeHttpAsync(object sender, Toolbelt.Blazor.HttpClientInterceptorEventArgs args)
    {
        var absPath = args.Request.RequestUri.AbsolutePath.ToLower();
        if (!absPath.Contains("login") && !absPath.Contains("refresh-token"))
        {
            try
            {
            }
            catch (Exception ex)
            {                    
                args.Cancel = true;
                _notify.Notify(new NotificationMessage()
                {
                    Severity = NotificationSeverity.Error,
                    Summary = "Error",
                    Detail = ex.Message ?? "Your session was expired",
                    Duration = 5000
                });
                //await _accountService.LogoutAsync();
                throw;
            }
        }
    }

    public void RegisterEvent()
    {
    }
     
    private Task _httpInterceptor_AfterSendAsync(object sender, HttpClientInterceptorEventArgs e)
    {
        return Task.CompletedTask;
    }
}
