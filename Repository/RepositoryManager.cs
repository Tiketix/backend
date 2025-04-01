using Contracts;

namespace Repository;

public sealed class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryContext _repositoryContext;
    private readonly Lazy<IEventRepository> _eventRepository;
    private readonly Lazy<IEmailVerificationTokenRepository> _emailVerificationTokenRepository;
    public RepositoryManager(RepositoryContext repositoryContext)
    {
        _repositoryContext = repositoryContext;
        _eventRepository = new Lazy<IEventRepository>(() => new
        EventRepository(repositoryContext));
        _emailVerificationTokenRepository = new Lazy<IEmailVerificationTokenRepository>(() => new
        EmailVerificationTokenRepository(repositoryContext));
    }
    public IEventRepository Event => _eventRepository.Value;
    public IEmailVerificationTokenRepository EmailVerificationToken => _emailVerificationTokenRepository.Value;
    public void Save() => _repositoryContext.SaveChanges();
}
