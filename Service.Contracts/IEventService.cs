using Entities.Models;

namespace Service.Contracts;

public interface IEventService
{
    IEnumerable<Event> GetAllEvents(bool trackChanges);
}
