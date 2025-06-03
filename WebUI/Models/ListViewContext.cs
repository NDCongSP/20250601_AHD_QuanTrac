namespace WebUI.Models;

public class ListViewContext<TItem>
{
    public Action<DetailViewData> OnAddNew { get; set; }
    public Action<DetailViewData> OnEdit { get; set; }
    public Action<DetailViewData> OnView { get; set; }
    //public Func<TItem, Task> OnDelete { get; set; }
    public bool NeedsRefresh { get; set; }
}

public class DetailViewContext<TItem>
{
    public TItem Item { get; set; }
    public string Title { get; set; }
    public Func<bool, Task> OnSaved { get; set; }
    public Func<TItem, Task> OnDelete { get; set; }
    public Func<bool, Task> OnCancelled { get; set; }
}

public class DetailViewData
{
    public string? Title { get; set; }
    public string? Id { get; set; }
    public DetailViewData() { }
    public DetailViewData(string? title, object? id)
    {
        Title = title;
        Id = id?.ToString();
    }
    public DetailViewData(string? title) { 
        Title = title;
    }
}