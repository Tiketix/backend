
using Entities.Models;

namespace Contracts
{
    public interface IEmailVerificationTokenRepository
    {
        EmailVerificationToken GetToken(string email, bool trackChanges);
        Task AddToken(EmailVerificationToken token);
        Task RemoveToken(EmailVerificationToken token);
    }

}

