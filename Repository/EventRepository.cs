using Contracts;
using Entities.Models;

namespace Repository
{
    internal sealed class EventRepository : RepositoryBase<Event>, IEventRepository
    {
        public EventRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public IEnumerable<Event> GetAllEvents(bool trackChanges) =>
            FindAll(trackChanges)
            .OrderBy(c => c.EventTitle)
            .ToList();

    }

}