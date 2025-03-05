using Entities.Models;

namespace Contracts
{
    public interface IEventRepository
    {
        IEnumerable<Event> GetAllEvents(bool trackChanges);
    }
}


