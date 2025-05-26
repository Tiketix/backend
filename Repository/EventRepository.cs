using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    internal sealed class EventRepository : RepositoryBase<Event>, IEventRepository
    {
        public EventRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public IEnumerable<Event> GetAllEvents(bool trackChanges) =>
            FindAll(trackChanges)
            .Include(e => e.EventCreator)
            .OrderBy(e => e.EventTitle)
            .ToList();

        public IEnumerable<Event> GetEventsByTime(string time, bool trackChanges) =>
            FindByCondition(e => e.EventTime == time, trackChanges)
            .Include(e => e.EventCreator)
            .OrderBy(e => e.EventTitle)
            .ToList();


        #pragma warning disable CS8603 // Possible null reference return.
        public Event GetEventByTitle(string title, bool trackChanges) =>
            FindByCondition(e => e.EventTitle == title, trackChanges)
            .Include(e => e.EventCreator)
            .SingleOrDefault();

        public Event GetEventById(Guid id, bool trackChanges) =>
            FindByCondition(e => e.Id == id, trackChanges)
            .SingleOrDefault();

        public void AddEvent(Event newEvent) => Create(newEvent);

        public void DeleteEvent(Event eventName) => Delete(eventName);

    } 

}