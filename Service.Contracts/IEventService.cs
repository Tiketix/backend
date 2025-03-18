using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface IEventService
{
    IEnumerable<EventDto> GetAllEvents(bool trackChanges);

    IEnumerable<EventDto> GetEventsByTime(string time, bool trackChanges);

    EventDto GetEventByTitle(string title, bool trackChanges);

    EventDto AddEvent(AddEventDto newEvent);
}
