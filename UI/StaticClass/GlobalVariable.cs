using Domain;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using RestEase;

namespace UI;

public static class GlobalVariable
{
    public static BreadCumb BreadCrumbData { get; set; } = new BreadCumb();
    public static BreadCumb BreadCrumbDataMaster { get; set; } = new BreadCumb();
    public static UserAuthorizationInfo UserAuthorizationInfo { get; set; } = new UserAuthorizationInfo();
    [CascadingParameter] public static AuthenticationState AuthenticationStateTask { get; set; } = null!;
    public static string FilePathTemporary { get; set; } = string.Empty;
    public static string ApiURL { get; set; } = string.Empty;
    public static bool AllowUpdateImageProduct { get; set; } = true;

    private static ConfigModel _configSystem;
    public static ConfigModel ConfigSystem
    {
        get => _configSystem ??= new ConfigModel();
        set => _configSystem = value;
    }

    public static async Task InitializeConfigAsync(IServiceProvider serviceProvider)
    {
        try
        {
            using var scope = serviceProvider.CreateScope();
            var ft01Service = scope.ServiceProvider.GetRequiredService<IFT01>();
            var navigationManager = scope.ServiceProvider.GetRequiredService<NavigationManager>();

            try
            {
                var response = await ft01Service.GetConfigAsync();
                _configSystem = response?.Data ?? new ConfigModel();
            }
            catch (ApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                // Không redirect, chỉ sử dụng config mặc định
                // Các trang cần authentication sẽ tự động redirect qua AuthorizeView
                Console.WriteLine($"Unauthorized khi tải config: {ex.Message}");
                _configSystem = new ConfigModel();
                return;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Lỗi khi tải cấu hình: {ex.Message}");
            _configSystem = new ConfigModel();
        }
    }
}