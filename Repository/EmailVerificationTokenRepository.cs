
using Contracts;
using Entities.Models;

namespace Repository
{
    public class EmailVerificationTokenRepository : RepositoryBase<EmailVerificationToken>, IEmailVerificationTokenRepository
    {
        public EmailVerificationTokenRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        #pragma warning disable CS8603 // Possible null reference return.
        public EmailVerificationToken GetToken(string email, bool trackChanges) =>
            FindByCondition(t => t.Email.Equals(email), trackChanges)
            .OrderBy(t => t.Id)
            .LastOrDefault();

        public async Task AddToken(EmailVerificationToken token) => await Create(token);

        public async Task RemoveToken(EmailVerificationToken token) => await Delete(token);
    }
}


