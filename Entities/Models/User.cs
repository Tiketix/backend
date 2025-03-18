using Microsoft.AspNetCore.Identity;

namespace Entities.Models
{
    public class User : IdentityUser
    {
        #pragma warning disable CS0114 // Member hides inherited member; missing override keyword
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
