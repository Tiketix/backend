namespace Shared.DataTransferObjects
{
    public record class EmailVerificationTokenDto(int Id, string Email, string Token, DateTime ExpiryTime, bool IsUsed);
}


