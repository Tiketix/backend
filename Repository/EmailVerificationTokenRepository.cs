
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
            .FirstOrDefault();

        public void AddToken(EmailVerificationToken token) => Create(token);

        public void RemoveToken(EmailVerificationToken token) => Delete(token);
    }
}


