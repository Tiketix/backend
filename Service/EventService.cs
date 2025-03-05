using Contracts;
using Entities.Models;
using Service.Contracts;

namespace Service;

internal sealed class EventService : IEventService
{
    private readonly IRepositoryManager _repository;
    
    public EventService(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public IEnumerable<Event> GetAllEvents(bool trackChanges)
    {
        try
        {
            var events = 
            _repository.Event.GetAllEvents(trackChanges);
            return events;
        }
        catch (Exception)
        {
            throw new Exception($"There is an error somewhere");
        }
    }
}

