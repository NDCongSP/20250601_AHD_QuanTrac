using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class MstUserSetting
    {
        [Key] public Guid Id { get; set; }
        public string UserId { get; set; }
        public string Currency { get; set; } = "USD";
    }
}
