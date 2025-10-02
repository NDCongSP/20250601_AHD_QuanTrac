using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("PermissionGroup")]
    public class PermissionGroup : IGenericEntity
    {
        [Key] public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? CreateOperatorId { get ; set ; }
        public DateTime? CreateAt { get ; set ; }
        public string? UpdateOperatorId { get ; set ; }
        public DateTime? UpdateAt { get ; set ; }
        public bool? IsDeleted { get ; set ; }
    }
}
