using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Permissions : GenericEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public EnumStatus Status { get; set; } = EnumStatus.Activated;
    }
}
