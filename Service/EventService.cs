using AutoMapper;
using Contracts;
using Entities.Models;
using Entities.Response;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service;

internal sealed class EventService : IEventService
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;

    
    public EventService(IRepositoryManager repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    

    public async Task<ApiResponse<IEnumerable<EventDto>>> GetAllEvents(bool trackChanges)
    {
        try
        {
            var events = await _repository.Event.GetAllEvents(trackChanges);
            var eventsDto = _mapper.Map<IEnumerable<EventDto>>(events);

            return ApiResponse<IEnumerable<EventDto>>.SuccessResponse(eventsDto, "Events retrieved successfully");
        }
        catch (Exception ex)
        {
            return ApiResponse<IEnumerable<EventDto>>.FailureResponse(new List<string> { ex.Message }, "Failed to retrieve events");
        }
    }

    public async Task<ApiResponse<IEnumerable<EventDto>>> GetEventsByTime(string time, bool trackChanges)
    {
        try
        {

            var events = await _repository.Event.GetEventsByTime(time, trackChanges);
            var eventsDto = _mapper.Map<IEnumerable<EventDto>>(events);
            return ApiResponse<IEnumerable<EventDto>>.SuccessResponse(eventsDto, "Events retrieved successfully");
        }
        catch (Exception ex)
        {
            return ApiResponse<IEnumerable<EventDto>>.FailureResponse(new List<string> { ex.Message }, "Failed to retrieve events");
        }
    }

    public async Task<ApiResponse<EventDto>> GetEventByTitle(string title, bool trackChanges)
    {
        try
        {
            var events = await _repository.Event.GetEventByTitle(title, trackChanges);
            var eventsDto = _mapper.Map<EventDto>(events);
            return ApiResponse<EventDto>.SuccessResponse(eventsDto, "Event retrieved successfully");
        }
        catch (Exception ex)
        {
            return ApiResponse<EventDto>.FailureResponse(new List<string> { ex.Message }, "Failed to retrieve event");
        }
        
    }

    public async Task<ApiResponse<EventDto>> AddEvent(AddEventDto newEvent)
    {
        if (newEvent is null)
            return ApiResponse<EventDto>.FailureResponse(new List<string> { "Event cannot be null" });
        if (string.IsNullOrWhiteSpace(newEvent.EventTitle))
            return ApiResponse<EventDto>.FailureResponse(new List<string> { "Event title cannot be empty" });

        var addNewEvent = _mapper.Map<Event>(newEvent);

        await _repository.Event.AddEvent(addNewEvent);
        await _repository.Save();

        var returnEvent = _mapper.Map<EventDto>(addNewEvent);
        return ApiResponse<EventDto>.SuccessResponse(returnEvent, "Event added successfully");
    }

    public async Task<ApiResponse<bool>> DeleteEvent(Guid id, bool trackChanges)
    {
        var eventExists = await _repository.Event.GetEventById(id, trackChanges);
        if (eventExists is null)
            return ApiResponse<bool>.FailureResponse(new List<string> { "Event does not exist." });

        await _repository.Event.DeleteEvent(eventExists);
        await _repository.Save();

        return ApiResponse<bool>.SuccessResponse(true, "Event deleted successfully");
    }


    public async Task<ApiResponse<EventDto>> UpdateEventDetails(Guid id, UpdateEventDetailsDto updateEventDetails, bool trackchanges)
    {
        var eventExists = await _repository.Event.GetEventById(id, trackchanges);
        if (eventExists is null)
            return ApiResponse<EventDto>.FailureResponse(new List<string> { "Event does not exist." });

        if (string.IsNullOrWhiteSpace(updateEventDetails.EventTitle))
            updateEventDetails.EventTitle = eventExists.EventTitle;
        if (string.IsNullOrWhiteSpace(updateEventDetails.EventDescription))
            updateEventDetails.EventDescription = eventExists.EventDescription;
        if (string.IsNullOrWhiteSpace(updateEventDetails.Location))
            updateEventDetails.Location = eventExists.Location;
        if (updateEventDetails.EventDate == null)
            updateEventDetails.EventDate = eventExists.EventDate;
        if (string.IsNullOrWhiteSpace(updateEventDetails.EventTime))
            updateEventDetails.EventTime = eventExists.EventTime;
        if (string.IsNullOrWhiteSpace(updateEventDetails.PhoneNo))
            updateEventDetails.PhoneNo = eventExists.Phone;
        if (updateEventDetails.TicketPrice == null)
            updateEventDetails.TicketPrice = eventExists.TicketPrice;
        if (updateEventDetails.TicketsAvailable == null)
            updateEventDetails.TicketsAvailable = eventExists.TicketsAvailable;


        _mapper.Map(updateEventDetails, eventExists);
        await _repository.Save();

        var updatedEvent = _mapper.Map<EventDto>(eventExists);
        return ApiResponse<EventDto>.SuccessResponse(updatedEvent, "Event updated successfully");
    }


}

