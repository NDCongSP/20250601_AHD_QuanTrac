using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class CourierApiHistoryDetail
    {
        [Key]
        public int Id { get; set; }
        public int HistoryId { get; set; }
        public int CompanyId { get; set; }
        public string CourierCd { get; set; }
        public string ApiParam { get; set; }
        public string ApiValue { get; set; }
    }
}
