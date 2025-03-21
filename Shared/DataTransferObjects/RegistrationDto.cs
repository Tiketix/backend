using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public record RegistrationDto
    {
        [Required(ErrorMessage = "FirstName is required")]
        public string? FirstName { get; init; }

        [Required(ErrorMessage = "LastName is required")]
        public string? LastName { get; init; }

        [Required(ErrorMessage = "Username is required")]
        public string? Username { get; init; }
        
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; init; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; init; }
        public string? PhoneNumber { get; init; }

        public ICollection<string>? Roles { get; init; }
    }
}
