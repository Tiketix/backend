using Contracts;

namespace Repository;

public sealed class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryContext _repositoryContext;
    private readonly Lazy<IEventRepository> _eventRepository;
    public RepositoryManager(RepositoryContext repositoryContext)
    {
        _repositoryContext = repositoryContext;
        _eventRepository = new Lazy<IEventRepository>(() => new
        EventRepository(repositoryContext));
        }
    public IEventRepository Event => _eventRepository.Value;
    public void Save() => _repositoryContext.SaveChanges();
}
