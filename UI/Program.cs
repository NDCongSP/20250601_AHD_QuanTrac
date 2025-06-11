using UI;
using Radzen;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using Application.Extentions;
using Application.Services.Authen.UI;
using Application.Services.Authen;
using Application.Services;
using Blazored.LocalStorage;
using System.Globalization;
using Blazored.SessionStorage;
using RestEase.HttpClientFactory;
using Microsoft.AspNetCore.Components.Authorization;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
var jsInterop = builder.Build().Services.GetRequiredService<IJSRuntime>();
var result = await jsInterop.InvokeAsync<string>("blazorCulture.get");
var culture = result ?? "ja-JP";
CultureInfo.DefaultThreadCurrentCulture = new CultureInfo(culture);
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo(culture);

builder.Services.AddRadzenComponents();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
//builder.Services.AddScoped<IAuthServices, AuthServices>();

builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>()
    .AddScoped(sp => (ApiAuthenticationStateProvider)sp.GetRequiredService<AuthenticationStateProvider>())
    .AddScoped(sp => (IAccessTokenProvider)sp.GetRequiredService<AuthenticationStateProvider>())
    .AddScoped<IAccessTokenProviderAccessor, AccessTokenProviderAccessor>()
    .AddScoped<AuthenticationHeaderHandler>();

builder.Services.AddHttpClientInterceptor();
builder.Services.AddScoped<IHttpInterceptorManager, HttpInterceptorManager>();

builder.Services.AddAuthorizationCore(b =>
{
    b.AddPolicy("Admin", p =>
    {
        p.RequireRole(ConstantExtention.Roles.WarehouseAdmin);
        //p.RequireClaim("Permission", "1");
    });

    b.AddPolicy("Staff", p =>
    {
        p.RequireRole(ConstantExtention.Roles.WarehouseStaff);
        p.RequireClaim("Permission", "Warehouse Staff");
    });

    b.AddPolicy("System", p =>
    {
        p.RequireRole(ConstantExtention.Roles.WarehouseSystem);
    });

    b.AddPolicy("AdminAndSystem", p =>
    {
        p.RequireRole(new string[] { ConstantExtention.Roles.WarehouseAdmin, ConstantExtention.Roles.WarehouseSystem });
    });

    b.AddPolicy("AdminAndStaff", p =>
    {
        p.RequireRole(ConstantExtention.Roles.WarehouseAdmin, ConstantExtention.Roles.WarehouseStaff);
    });
});

builder.Services.AddCascadingAuthenticationState();
var config = builder.Configuration;
var url = config["ApiUrl:ApiBaseUrl"];

builder.Services.AddHttpClient("UI")
    .ConfigureHttpClient((sp, x) =>
    {
        x.BaseAddress = new Uri(url);
        x.EnableIntercept(sp);
    })
    .AddHttpMessageHandler<AuthenticationHeaderHandler>()
    .UseWithRestEaseClient<IAccount>()
    .UseWithRestEaseClient<IPermissions>()
    .UseWithRestEaseClient<IRoleToPermissions>()
    .UseWithRestEaseClient<ITenants>();

builder.Services.AddScoped<HttpClient>(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("UI"));
builder.Services.AddBlazoredLocalStorage();


builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
builder.Logging.SetMinimumLevel(LogLevel.Warning);


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Logging.SetMinimumLevel(LogLevel.Warning);

await builder.Build().RunAsync();
