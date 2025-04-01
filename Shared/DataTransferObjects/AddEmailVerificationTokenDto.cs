namespace Shared.DataTransferObjects
{
    public record class AddEmailVerificationTokenDto
                        (string Email, string Token, DateTime ExpiryTime, bool IsUsed);
}


