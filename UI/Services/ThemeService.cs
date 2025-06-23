using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace UI.Services
{
    public class ThemeService
    {
        private readonly IJSRuntime _jsRuntime;
        public bool IsDarkMode { get; private set; }
        public event Action OnChange;

        public ThemeService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task InitializeAsync()
        {
            var theme = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "theme");
            IsDarkMode = theme == "dark";
        }

        public async Task ToggleThemeAsync()
        {
            IsDarkMode = !IsDarkMode;
            var theme = IsDarkMode ? "dark" : "light";
            await _jsRuntime.InvokeVoidAsync("setTheme", theme);
            OnChange?.Invoke();
        }
    }
}
