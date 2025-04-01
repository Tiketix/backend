using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class EmailVerificationToken
    {
        [Column("TokenId")]
        public int Id { get; set; }
        public required string Email { get; set; }
        public required string Token { get; set; }
        public DateTime ExpiryTime { get; set; }
        public bool IsUsed { get; set; } = false;
    }

}

