namespace Domain
{
    /// <summary>
    /// Chứa thông tin cần hiển thị của 1 trạm.
    /// </summary>
    public class RealtimeDisplayModel
    {
        public int LocationId { get; set; } = 1;
        public List<TagsStation> Stations { get; set; } = new List<TagsStation>();
        
        public CalculatorValueModel CalculatorValue { get; set; } = new CalculatorValueModel();
    }
}
