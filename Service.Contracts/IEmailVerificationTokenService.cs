
using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface IEmailVerificationTokenService
    {
        EmailVerificationTokenDto GetToken(string email, bool trackChanges);
        EmailVerificationTokenDto AddToken(AddEmailVerificationTokenDto token);
    }

}

