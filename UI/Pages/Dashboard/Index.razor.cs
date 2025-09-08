//using System.Timers;
//using Application.Services;
//using Domain.NotTable;
//using Microsoft.AspNetCore.Components;
//using Timer = System.Timers.Timer;

//namespace UI.Pages.Dashboard;

//public partial class Index : IDisposable
//{
//    [Inject] 
//    private IFT02 Ft02Service { get; set; } = null!;
    
//    private RealtimeDisplayModel? _realtimeData;
//    private bool _isLoading = true;
//    private string _errorMessage = string.Empty;
//    private Timer? _refreshTimer;
//    private const int RefreshIntervalMs = 10000; // 10 seconds

//    protected override async Task OnInitializedAsync()
//    {
//        await LoadRealtimeData();
        
//        // Setup timer for auto-refresh
//        _refreshTimer = new Timer(RefreshIntervalMs);
//        _refreshTimer.Elapsed += async (sender, e) => await RefreshData();
//        _refreshTimer.AutoReset = true;
//        _refreshTimer.Enabled = true;
//    }

//    private async Task LoadRealtimeData()
//    {
//        try
//        {
//            _isLoading = true;
//            StateHasChanged();
            
//            var result = await Ft02Service.GetFirstOrDefaultRealTimeDisplay();
//            _realtimeData = result?.FirstOrDefault();
            
//            _errorMessage = string.Empty;
//        }
//        catch (Exception ex)
//        {
//            _errorMessage = $"Error loading real-time data: {ex.Message}";
//            Console.Error.WriteLine(_errorMessage);
//        }
//        finally
//        {
//            _isLoading = false;
//            await InvokeAsync(StateHasChanged);
//        }
//    }

//    private async Task RefreshData()
//    {
//        await InvokeAsync(async () =>
//        {
//            await LoadRealtimeData();
//        });
//    }

//    public void Dispose()
//    {
//        _refreshTimer?.Stop();
//        _refreshTimer?.Dispose();
//        GC.SuppressFinalize(this);
//    }
//}
