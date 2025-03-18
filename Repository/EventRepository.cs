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
            .OrderBy(e => e.EventTitle)
            .ToList();

        public IEnumerable<Event> GetEventsByTime(string time, bool trackChanges) =>
            FindByCondition(e => e.EventTime == time, trackChanges)
            .OrderBy(e => e.EventTitle)
            .ToList();


        #pragma warning disable CS8603 // Possible null reference return.
        public Event GetEventByTitle(string title, bool trackChanges) =>
            FindByCondition(e => e.EventTitle == title, trackChanges)
            .SingleOrDefault();

        public void AddEvent(Event newEvent) => Create(newEvent);

    } 

}