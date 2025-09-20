using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Domain;
using Application.Services;

namespace UI
{
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

    // ... other properties ...

        public static async Task InitializeConfigAsync(IServiceProvider serviceProvider)
    {
        try
        {
            using var scope = serviceProvider.CreateScope();
            var ft01Service = scope.ServiceProvider.GetRequiredService<IFT01>();
            var response = await ft01Service.GetConfigAsync();
            _configSystem = response?.Data ?? new ConfigModel();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Lỗi khi tải cấu hình: {ex.Message}");
            _configSystem = new ConfigModel();
        }
    }
    }
}
