using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("PermissionGroup")]
    public class PermissionGroup : GenericEntity
    {
        [Key] public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
