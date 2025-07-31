using Contracts;

namespace Repository;

public sealed class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryContext _repositoryContext;
    private readonly Lazy<IEventRepository> _eventRepository;
    private readonly Lazy<ITicketRepository> _ticketRepository;
    private readonly Lazy<IEmailVerificationTokenRepository> _emailVerificationTokenRepository;
    public RepositoryManager(RepositoryContext repositoryContext)
    {
        _repositoryContext = repositoryContext;
        _eventRepository = new Lazy<IEventRepository>(() => new
        EventRepository(repositoryContext));
        _ticketRepository = new Lazy<ITicketRepository>(() => new
        TicketRepository(repositoryContext));
        _emailVerificationTokenRepository = new Lazy<IEmailVerificationTokenRepository>(() => new
        EmailVerificationTokenRepository(repositoryContext));
    }
    public IEventRepository Event => _eventRepository.Value;
    public ITicketRepository Ticket => _ticketRepository.Value;
    public IEmailVerificationTokenRepository EmailVerificationToken => _emailVerificationTokenRepository.Value;
    public async Task Save() => await _repositoryContext.SaveChangesAsync();
}
