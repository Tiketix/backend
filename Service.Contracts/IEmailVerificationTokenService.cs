
using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface IEmailVerificationTokenService
    {
        Task<EmailVerificationTokenDto> GetToken(string email, bool trackChanges);
        Task<EmailVerificationTokenDto> AddToken(AddEmailVerificationTokenDto token);
    }

}

