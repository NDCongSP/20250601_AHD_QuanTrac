namespace UI.Services
{
    public class LayoutStateService
    {
        public string? Title { get; private set; }
        public string? Subtitle { get; private set; }

        public event Action? OnChange;

        public void SetTitle(string title, string? subtitle = null)
        {
            Title = title;
            Subtitle = subtitle;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
