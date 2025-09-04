using System.Collections.Generic;

namespace Domain
{
    /// <summary>
    /// Chứa thông tin cần hiển thị của 1 trạm.
    /// </summary>
    public class RealtimeDisplayModel 
    {
        
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public List<TagsStation> Stations { get; set; } = new List<TagsStation>();

        public CalculatorValueModel CalculatorValue { get; set; } = new CalculatorValueModel();
        
    }
}
