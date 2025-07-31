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

        public async Task<IEnumerable<Event>> GetAllEvents(bool trackChanges) =>
            await FindAll(trackChanges)
            .Include(e => e.EventCreator)
            .OrderBy(e => e.EventTitle)
            .ToListAsync();

        public async Task<IEnumerable<Event>> GetEventsByTime(string time, bool trackChanges) =>
            await FindByCondition(e => e.EventTime == time, trackChanges)
            .Include(e => e.EventCreator)
            .OrderBy(e => e.EventTitle)
            .ToListAsync();


        #pragma warning disable CS8603 // Possible null reference return.
        public async Task<Event> GetEventByTitle(string title, bool trackChanges) =>
            await FindByCondition(e => e.EventTitle == title, trackChanges)
            .Include(e => e.EventCreator)
            .SingleOrDefaultAsync();

        public async Task<Event> GetEventById(Guid id, bool trackChanges) =>
            await FindByCondition(e => e.Id == id, trackChanges)
            .SingleOrDefaultAsync();

        public async Task AddEvent(Event newEvent) => await Create(newEvent);

        public async Task DeleteEvent(Event eventName) => await Delete(eventName);

    } 

}