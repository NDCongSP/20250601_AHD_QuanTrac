using System.ComponentModel.DataAnnotations;
using Domain.Commons;

namespace Domain.Entities
{
    public class CourierApiHistory : AuditEntityBase
    {
        [Key]
        public int Id { get; set; }
    }
}
