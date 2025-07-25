using Domain.Enums;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;

namespace UI.Shared;

public partial class ListDetail<TModel> : ComponentBase where TModel : new()
{
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Parameter] public string BaseRoute { get; set; } = string.Empty;
    [Parameter] public string BaseTitle { get; set; } = string.Empty;
    [Parameter] public RenderFragment<TModel> DetailTemplate { get; set; } = default!;
    [Parameter] public RenderFragment ListTemplate { get; set; } = default!;
    [Parameter] public EventCallback<bool> OnReloadChanged { get; set; }

    protected bool IsShowDetail { get; set; }
    protected bool IsReload { get; set; }
    public EnumMode DetailMode { get; set; } = EnumMode.Create;
    public string? ItemId { get; set; } = string.Empty;
    protected TModel CurrentModel { get; set; } = new();

    protected override void OnInitialized()
    {
        base.OnInitialized();
        NavigationManager.LocationChanged += HandleLocationChanged;
        ParseUrl(NavigationManager.Uri);
    }

    private void HandleLocationChanged(object? sender, Microsoft.AspNetCore.Components.Routing.LocationChangedEventArgs e)
    {
        ParseUrl(e.Location);
    }

    private void ParseUrl(string url)
    {
        if (string.IsNullOrEmpty(BaseRoute) || !url.Contains(BaseRoute))
            return;

        var uri = new Uri(url);
        var query = QueryHelpers.ParseQuery(uri.Query);

        if (query.TryGetValue("mode", out StringValues modeValues) && 
            query.TryGetValue("id", out StringValues idValues))
        {
            var mode = modeValues.FirstOrDefault();
            var id = idValues.FirstOrDefault();

            if (Enum.TryParse<EnumMode>(mode, true, out var enumMode) && !string.IsNullOrEmpty(id))
            {
                DetailMode = enumMode;
                ItemId = id;
                // Note: You'll need to load the model based on the ID here
                // This is just a placeholder - you'll need to implement the actual model loading
                CurrentModel = new TModel();
                IsShowDetail = true;
                StateHasChanged();
            }
        }
    }

    public void HandleCreate()
    {
        DetailMode = EnumMode.Create;
        CurrentModel = new TModel();
        IsShowDetail = true;
        LayoutState.SetTitle($"{BaseTitle} - Thêm mới");
        //UpdateUrl();
    }

    public void HandleEdit(string? id, TModel model)
    {
        DetailMode = EnumMode.Edit;
        ItemId = id;
        CurrentModel = model;
        IsShowDetail = true;
        LayoutState.SetTitle($"{BaseTitle} - Chỉnh sửa");
        //UpdateUrl();
    }

    public void HandleBack()
    {
        IsShowDetail = false;
        LayoutState.SetTitle(BaseTitle);
        //ClearUrl();
    }

    public async Task HandleSaved()
    {
        IsReload = true;
        IsShowDetail = false;
        //ClearUrl();
        await OnReloadChanged.InvokeAsync(true);
    }

    private void UpdateUrl()
    {
        if (string.IsNullOrEmpty(BaseRoute))
            return;

        var uri = new Uri(NavigationManager.Uri);
        var baseUri = uri.GetLeftPart(UriPartial.Path);
        
        var query = new Dictionary<string, string>
        {
            ["mode"] = DetailMode.ToString().ToLower(),
            ["id"] = ItemId ?? string.Empty
        };

        var newUrl = QueryHelpers.AddQueryString(baseUri, query);
        NavigationManager.NavigateTo(newUrl, true);
    }

    private void ClearUrl()
    {
        if (string.IsNullOrEmpty(BaseRoute))
            return;

        var uri = new Uri(NavigationManager.Uri);
        var baseUri = uri.GetLeftPart(UriPartial.Path);
        
        if (uri.Query != string.Empty)
        {
            NavigationManager.NavigateTo(baseUri, false);
        }
    }

    public void Dispose()
    {
        NavigationManager.LocationChanged -= HandleLocationChanged;
    }
}
