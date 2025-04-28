using Entities.Models;

namespace Contracts
{
    public interface IEventRepository
    {
        IEnumerable<Event> GetAllEvents(bool trackChanges);

        IEnumerable<Event> GetEventsByTime(string time, bool trackChanges);

        Event GetEventByTitle(string title, bool trackChanges);
        Event GetEventById(Guid id, bool trackChanges);

        void AddEvent(Event newEvent);

        void DeleteEvent(Event eventName);
    }
}


