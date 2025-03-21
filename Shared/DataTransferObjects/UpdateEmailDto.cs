
using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public class UpdateEmailDto
    {
        [Required]
        [EmailAddress]
        public required string CurrentEmail { get; set; }

        [Required]
        public required string Password { get; set; }
        
        [Required]
        [EmailAddress]
        public required string NewEmail { get; set; }
    }
}
