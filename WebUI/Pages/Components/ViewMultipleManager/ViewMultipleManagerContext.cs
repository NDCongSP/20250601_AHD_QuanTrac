namespace WebUI.Pages.Components.ViewMultipleManager;

public class DetailViewData
{
    public string Id { get; set; }
    public string Title { get; set; }
    public DetailViewType ViewType { get; set; }
}

public class ListViewContext<TItem>
{
    public Action<DetailViewData, DetailViewType> OnAddNew { get; set; }
    public Action<DetailViewData, DetailViewType> OnEdit { get; set; }
    public Action<DetailViewData, DetailViewType> OnView { get; set; }
    public bool NeedsRefresh { get; set; }
}

public class DetailViewContext<TItem>
{
    public string Title { get; set; }
    public TItem Item { get; set; }
    public Func<bool, Task> OnSaved { get; set; }
    public Func<TItem, Task> OnDelete { get; set; }
    public Func<bool, Task> OnCancelled { get; set; }
}
public enum DetailViewType
{
    ViewDetail1,
    ViewDetail2,
}