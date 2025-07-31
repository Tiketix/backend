using Entities.Response;
using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface IEventService
{
    Task<ApiResponse<IEnumerable<EventDto>>> GetAllEvents(bool trackChanges);
    Task<ApiResponse<IEnumerable<EventDto>>> GetEventsByTime(string time, bool trackChanges);
    Task<ApiResponse<EventDto>> GetEventByTitle(string title, bool trackChanges);
    Task<ApiResponse<EventDto>> AddEvent(AddEventDto newEvent);
    Task<ApiResponse<EventDto>> UpdateEventDetails(Guid id, UpdateEventDetailsDto updateEventDetails, bool trackChanges);
    Task<ApiResponse<bool>> DeleteEvent(Guid id, bool trackChanges);
}
