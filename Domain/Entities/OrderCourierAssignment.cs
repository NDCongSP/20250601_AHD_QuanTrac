using System.ComponentModel.DataAnnotations;
using Domain.Commons;

namespace Domain.Entities
{
    public class OrderCourierAssignment : AuditEntityBase
    {
        [Key]
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string OrderId { get; set; }
        public string ChannelCode { get; set; }
        public string OrderName { get; set; }
        public string DeliveryCompanyCode { get; set; }
        public string DataKey { get; set; }
    }
}
