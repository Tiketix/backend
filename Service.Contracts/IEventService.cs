using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface IEventService
{
    IEnumerable<EventDto> GetAllEvents(bool trackChanges);
    IEnumerable<EventDto> GetEventsByTime(string time, bool trackChanges);
    EventDto GetEventByTitle(string title, bool trackChanges);
    EventDto AddEvent(AddEventDto newEvent);
    void UpdateEventDetails(string title, UpdateEventDetailsDto updateEventDetails, bool trackChanges);

    void DeleteEvent(Guid id, bool trackchanges);
}
