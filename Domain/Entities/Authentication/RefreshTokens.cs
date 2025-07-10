using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("RefreshTokens", Schema = "wms")]
    public class RefreshTokens
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        [Key] public string RefreshToken { get; set; }
        public DateTime ExpirationTime { get; set; }
        public bool? Activated { get; set; } = true;
    }
}
