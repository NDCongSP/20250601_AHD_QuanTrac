using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;

namespace UI.Services
{
    public class AppInitializer
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly NavigationManager _navigationManager;

        public AppInitializer(IServiceProvider serviceProvider, NavigationManager navigationManager)
        {
            _serviceProvider = serviceProvider;
            _navigationManager = navigationManager;
        }

        public async Task InitializeConfigAsync()
        {
            // Only initialize if not on the login page
            var currentUrl = _navigationManager.Uri;
            if (!currentUrl.EndsWith("/login", StringComparison.OrdinalIgnoreCase) && 
                !currentUrl.EndsWith("/login/", StringComparison.OrdinalIgnoreCase))
            {
                await GlobalVariable.InitializeConfigAsync(_serviceProvider);
            }
        }
    }
}
