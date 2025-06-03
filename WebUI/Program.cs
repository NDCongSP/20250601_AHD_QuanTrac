using Application.Services;
using Application.Services.Authen;
using Application.Services.Authen.UI;
using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using RestEase.HttpClientFactory;
using System.Globalization;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using WebUI;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//// Thêm dịch vụ Localization
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
//builder.Services.AddLocalization();

// Lấy ngôn ngữ đã lưu trong localStorage
var jsInterop = builder.Build().Services.GetRequiredService<IJSRuntime>();
var result = await jsInterop.InvokeAsync<string>("blazorCulture.get");
var culture = result ?? "ja-JP";  // Nếu không tìm thấy ngôn ngữ trong localStorage, mặc định là "ja-JP"
//var culture = "ja-JP";

// Thiết lập ngôn ngữ cho ứng dụng
CultureInfo.DefaultThreadCurrentCulture = new CultureInfo(culture);
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo(culture);


var config = builder.Configuration;

#region BreadCumb initial
//GlobalVariable.BreadCrumbDataMaster.Add(new BreadCrumbModel()
//{
//    Path = "User Manager",
//    Text= "Users List|"
//});
#endregion

//add dịch vụ để truyền data model từ master vào details.
builder.Services.AddSingleton<MasterTransferToDetails>();

builder.Services.AddRadzenComponents();
//builder.Services.AddBlazorBootstrap();

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
builder.Services.AddScoped<IAuthServices, AuthServices>();

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
        //p.RequireClaim("Permission", "1");
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

var url = config["ApiUrl:ApiBaseUrl"];
GlobalVariable.ApiURL = url;
Console.WriteLine($"API URL: {url}");
//Debug.WriteLine($"File path: {GlobalVariable.FilePathTemporary}");
GlobalVariable.AllowUpdateImageProduct = bool.TryParse(config["AllowUpdateImageProduct"], out bool value) ? value : true;

builder.Services.AddScoped<TokenRetrievalHandler>();
//Register client and services use RestEase library
// Register the RestEase client
builder.Services.AddHttpClient("WebUI")
    .ConfigureHttpClient((sp, x) =>
    {
        x.BaseAddress = new Uri(url);
        x.EnableIntercept(sp);
    })
    .AddHttpMessageHandler<AuthenticationHeaderHandler>()

    .AddPolicyHandler((sp, request) => RetryRefreshTokenHandler.GetTokenRefresher(sp, request))
    .AddHttpMessageHandler<TokenRetrievalHandler>()

    .UseWithRestEaseClient<IPermissions>()
    .UseWithRestEaseClient<IRoleToPermissions>()
    .UseWithRestEaseClient<ITenants>()
    .UseWithRestEaseClient<IUserToTenant>();

builder.Services.AddScoped<HttpClient>(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("WebUI"));
builder.Services.AddBlazoredLocalStorage();


builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
builder.Logging.SetMinimumLevel(LogLevel.Warning);

await builder.Build().RunAsync();
