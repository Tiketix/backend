
using Entities.Models;

namespace Contracts
{
    public interface IEmailVerificationTokenRepository
    {
        EmailVerificationToken GetToken(string email, bool trackChanges);
        void AddToken(EmailVerificationToken token);
        void RemoveToken(EmailVerificationToken token);
    }

}

