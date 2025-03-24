using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public class UpdateUserPasswordDto
    {
    

        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        
        [Required]
        public required string CurrentPassword { get; set; }
        
        [Required]
        [MinLength(8)]
        public required string NewPassword { get; set; }

    }
}

