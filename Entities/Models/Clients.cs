using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Client 
    {
        [Column("ClientId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "First Name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public required string FirstName { get; set; }
        
        [Required(ErrorMessage = "Last Name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name 60 characters.")]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "Email is a required field.")]
        [MaxLength(100, ErrorMessage = "Maximum length for the Email is 100 characters.")]
        public required string Email { get; set; }
        
        [MinLength(10, ErrorMessage = "Minimum length for your phone number is 10 characters.")]
        [MaxLength(20, ErrorMessage = "Maximum length for your phone number is 20 characters.")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "You need a password.")]
        [MinLength(6, ErrorMessage = "Minimum length for your password is 6 characters.")]
        public required string Password { get; set; }

    }
}
