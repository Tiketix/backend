using Entities.Models;

namespace Contracts
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetAllEvents(bool trackChanges);

        Task<IEnumerable<Event>> GetEventsByTime(string time, bool trackChanges);

        Task<Event> GetEventByTitle(string title, bool trackChanges);
        Task<Event> GetEventById(Guid id, bool trackChanges);

        Task AddEvent(Event newEvent);

        Task DeleteEvent(Event eventName);
    }
}


