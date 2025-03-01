using Microsoft.AspNetCore.Identity;

namespace Identity.Core.Entites
{
    public class ApplicationUser: IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime DateCreated { get; set; }
        public string? RoleId { get; set; }
        public string? Role { get; set; }
        public string? UserClaim { get; set; }
    }
}
