
namespace Shared.DataTransferObjects
{
    public class ConfirmEmailDto
    {
        public required string Email { get; set; }
        public required string Token { get; set; }
    }
}


