using Application.Services.Authen;
using Application.Services.Authen.UI;
using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using RestEase.HttpClientFactory;
using System.Globalization;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using UI;
using UI.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Add localization
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

// Build a temporary service provider to get the IJSRuntime
var host = builder.Build();
var jsInterop = host.Services.GetRequiredService<IJSRuntime>();

// Get the culture from localStorage or use default 'vi-VN'
var result = await jsInterop.InvokeAsync<string>("localStorage.getItem", "culture");
var cultureName = result ?? "vi-VN";

// Lấy bản sao culture vi-VN
var customCulture = new CultureInfo(cultureName);

// Thay dấu thập phân sang "."
customCulture.NumberFormat.NumberDecimalSeparator = ".";
customCulture.NumberFormat.NumberGroupSeparator = ","; // nhóm hàng nghìn vẫn dùng dấu phẩy

// Áp dụng culture này
CultureInfo.DefaultThreadCurrentCulture = customCulture;
CultureInfo.DefaultThreadCurrentUICulture = customCulture;

builder.Services.AddRadzenComponents();
builder.Services.AddScoped<UI.Services.LayoutStateService>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddBlazoredSessionStorage();

builder.Services.AddHttpClientInterceptor();

builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>()
    .AddScoped(sp => (ApiAuthenticationStateProvider)sp.GetRequiredService<AuthenticationStateProvider>())
    .AddScoped(sp => (IAccessTokenProvider)sp.GetRequiredService<AuthenticationStateProvider>())
    .AddScoped<IAccessTokenProviderAccessor, AccessTokenProviderAccessor>()
    .AddScoped<AuthenticationHeaderHandler>();

builder.Services.AddScoped<IHttpInterceptorManager, HttpInterceptorManager>();


builder.Services.AddAuthorizationCore(b =>
{
    b.AddPolicy("Admin", p =>
    {
        p.RequireRole(ConstantExtention.Roles.Admin);
        //p.RequireClaim("Permission", "1");
    });

    b.AddPolicy("Operator", p =>
    {
        p.RequireRole(ConstantExtention.Roles.Operator);
        p.RequireClaim("Permission", "Operator");
    });

    b.AddPolicy("System", p =>
    {
        p.RequireRole(ConstantExtention.Roles.System);
    });

    b.AddPolicy("AdminAndSystem", p =>
    {
        p.RequireRole(new string[] { ConstantExtention.Roles.Admin, ConstantExtention.Roles.System });
    });

    b.AddPolicy("AdminAndOperator", p =>
    {
        p.RequireRole(ConstantExtention.Roles.Admin, ConstantExtention.Roles.Operator);
    });
});

builder.Services.AddCascadingAuthenticationState();
var config = builder.Configuration;
var url = config["ApiUrl:ApiBaseUrl"];
builder.Services.AddScoped<TokenRetrievalHandler>();

builder.Services.AddHttpClient("UI")
    .ConfigureHttpClient((sp, x) =>
    {
        x.BaseAddress = new Uri(url);
        x.EnableIntercept(sp);
    })
    .AddHttpMessageHandler<AuthenticationHeaderHandler>()
    .AddPolicyHandler((sp, request) => RetryRefreshTokenHandler.GetTokenRefresher(sp, request))
    .AddHttpMessageHandler<TokenRetrievalHandler>()

    .UseWithRestEaseClient<IAccount>()
    .UseWithRestEaseClient<IPermissions>()
    .UseWithRestEaseClient<IRoleToPermissions>()
    .UseWithRestEaseClient<IFT01>()
    .UseWithRestEaseClient<IFT02>()
    .UseWithRestEaseClient<IFT03>()
    .UseWithRestEaseClient<IFT05>()
    .UseWithRestEaseClient<IFT06>()
    .UseWithRestEaseClient<IFT07>()
    .UseWithRestEaseClient<IScadaUser>();

// Register the initialization service
builder.Services.AddScoped<AppInitializer>();

var app = builder.Build();

// Get the navigation manager
var navigationManager = app.Services.GetRequiredService<NavigationManager>();

// Only initialize config if not on the login page
if (!navigationManager.Uri.EndsWith("/login", StringComparison.OrdinalIgnoreCase) && 
    !navigationManager.Uri.EndsWith("/login/", StringComparison.OrdinalIgnoreCase))
{
    await app.Services.GetRequiredService<AppInitializer>().InitializeConfigAsync();
}

builder.Services.AddScoped<HttpClient>(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("UI"));
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<UI.Services.ThemeService>();

builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
builder.Logging.SetMinimumLevel(LogLevel.Warning);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Logging.SetMinimumLevel(LogLevel.Warning);

await builder.Build().RunAsync();
