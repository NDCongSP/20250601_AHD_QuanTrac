using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("RoleToPermission")]
    public class RoleToPermission : GenericEntity
    {
        [Key] public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public  string RoleName { get; set; }
        public Guid PermissionId { get; set; }
        public string? PermisionName { get; set; }
        public string PermisionDescription { get; set; }
        public EnumStatus Status { get; set; } = EnumStatus.Activated;
        public string? CreateOperatorId { get ; set ; }
        public DateTime? CreateAt { get ; set ; }
        public string? UpdateOperatorId { get ; set ; }
        public DateTime? UpdateAt { get ; set ; }
        public bool? IsDeleted { get ; set ; }
    }
}
