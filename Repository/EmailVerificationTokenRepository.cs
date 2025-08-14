
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class EmailVerificationTokenRepository : RepositoryBase<EmailVerificationToken>, IEmailVerificationTokenRepository
    {
        public EmailVerificationTokenRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        #pragma warning disable CS8603 // Possible null reference return.
        public async Task<EmailVerificationToken> GetToken(string email, bool trackChanges) =>
            await FindByCondition(t => t.Email.Equals(email), trackChanges)
            .OrderBy(t => t.Id)
            .LastOrDefaultAsync();

        public async Task AddToken(EmailVerificationToken token) => await Create(token);

        public async Task RemoveToken(EmailVerificationToken token) => await Delete(token);
    }
}


