using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Devices")]
    public class Device : GenericEntity
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string? Type { get; set; }
        public string? Model { get; set; }
        public string? ActiveUser { get; set; }

        public string? Description { get; set; }
        public string? OS { get; set; }
        public string? CPU { get; set; }
        public string? Memory { get; set; }
        public EnumStatus Status { get; set; } = EnumStatus.Activated;
        public string? CurrentVersionCommon { get; set; }
        public string? CurrentVersion { get; set; }
    }
}
