namespace Shared.DataTransferObjects
{
    public class PasswordResetDto
    {
        public required string Email { get; set; }

    }
    public class PasswordReset
    {
        public required string Token { get; set; }
        public required string Email { get; set; }
        public required string NewPassword { get; set; }
    }

    // public class ValidateTokenDto
    // {
    //     public required string Email { get; set; }
    //     public required string Token { get; set; }
    // }

}