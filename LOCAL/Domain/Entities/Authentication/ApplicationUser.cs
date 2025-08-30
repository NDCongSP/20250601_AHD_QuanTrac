using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
        public string? Localtion { get; set; }
        public EnumStatus? Status { get; set; }
    }
}
