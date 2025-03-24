using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public class UpdatePhoneDto
    {
        [Required]
        [EmailAddress]
        public required string CurrentEmail { get; set; }
        
        [Required]
        public required string Password { get; set; }
        
        [Required]
        public required string PhoneNumber { get; set; }
    }
}


