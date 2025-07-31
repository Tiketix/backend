using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public record RegistrationDto
    {
        [Required(ErrorMessage = "FirstName is required")]
        public required string FirstName { get; init; }

        [Required(ErrorMessage = "LastName is required")]
        public required string LastName { get; init; }

        [Required(ErrorMessage = "Username is required")]
        public required string Username { get; init; }

        [Required(ErrorMessage = "Email is required")]
        public required string Email { get; init; }

        [Required(ErrorMessage = "Password is required")]
        public required string Password { get; init; }
        public string? PhoneNumber { get; init; }
        public ICollection<string> Roles { get; set; } = new List<string>();
    }
}
