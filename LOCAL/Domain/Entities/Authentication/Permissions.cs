using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Permissions")]
    public class Permissions : GenericEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public EnumStatus Status { get; set; } = EnumStatus.Activated;
        public string? CreateOperatorId { get ; set ; }
        public DateTime? CreateAt { get ; set ; }
        public string? UpdateOperatorId { get ; set ; }
        public DateTime? UpdateAt { get ; set ; }
        public bool? IsDeleted { get ; set ; }
    }
}
