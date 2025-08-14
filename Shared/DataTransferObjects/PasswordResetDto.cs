namespace Shared.DataTransferObjects
{
    public class RequestPasswordResetDto
    {
        public required string Email { get; set; }

    }
    public class PasswordReset
    {
        public required string Token { get; set; }
        public required string Email { get; set; }
        public required string NewPassword { get; set; }
    }

    public class UpdateUserPasswordDto
    {
        public required string Email { get; set; }
        public required string CurrentPassword { get; set; }
        public required string NewPassword { get; set; }
    }

    // public class ValidateTokenDto
    // {
    //     public required string Email { get; set; }
    //     public required string Token { get; set; }
    // }

}