using Domain.Commons;

namespace Domain.Entities
{
    public class CourierApi : AuditEntityBase
    {
        public int CompanyId { get; set; }
        public string CourierCd { get; set; }
        public string ApiParam { get; set; }
        public string ApiValue { get; set; }
    }
}
