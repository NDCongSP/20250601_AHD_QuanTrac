using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("PermissionsTenant", Schema = "wms")]
    public class PermissionsTenant: GenericEntity
    {
        [Key] public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public EnumStatus Status { get; set; } = EnumStatus.Activated;
    }
}
