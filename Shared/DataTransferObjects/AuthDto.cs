using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public record AuthDto
    {
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; init; }
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; init; }
    }

}

