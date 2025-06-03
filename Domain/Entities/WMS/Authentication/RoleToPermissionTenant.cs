using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("RoleToPermissionTenant")]
    public class RoleToPermissionTenant : GenericEntity
    {
        [Key] public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
        public Guid PermissionTenantId { get; set; }
        public string? PermissionTenantName { get; set; }
        public string? PermissionTenantDesciption { get; set; }
        public EnumStatus Status { get; set; } = EnumStatus.Activated;
    }
}
