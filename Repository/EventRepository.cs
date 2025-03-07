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


        #pragma warning disable CS8603 // Possible null reference return.
        public Event GetEventByTitle(string title, bool trackChanges) =>
            FindByCondition(e => e.EventTitle == title, trackChanges)
            .SingleOrDefault();

    } 

}