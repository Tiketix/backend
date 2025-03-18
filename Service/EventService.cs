using AutoMapper;
using Contracts;
using Entities.Models;
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

    

    public IEnumerable<EventDto> GetAllEvents(bool trackChanges)
    {
        try
        {
            // var events = 
            // _repository.Event.GetAllEvents(trackChanges);
            // return events; //this was before DTO introduction

            var events = _repository.Event.GetAllEvents(trackChanges);
            var eventsDto = _mapper.Map<IEnumerable<EventDto>>(events);
                            return eventsDto;
        }
        catch (Exception)
        {
            throw new Exception($"There is an error somewhere");
        }
    }

    public IEnumerable<EventDto> GetEventsByTime(string time, bool trackChanges)
    {
        try
        {

            var events = _repository.Event.GetEventsByTime(time, trackChanges);
            var eventsDto = _mapper.Map<IEnumerable<EventDto>>(events);
                            return eventsDto;
        }
        catch (Exception)
        {
            throw new Exception($"Error retrieving Events");
        }
    }

    public EventDto GetEventByTitle(string title, bool trackChanges)
    {
        try
        {
            var events = _repository.Event.GetEventByTitle(title, trackChanges);
        
            var eventsDto = _mapper.Map<EventDto>(events);
                        
            return eventsDto;
        }
        catch (Exception)
        {
            throw new Exception("There is an error somewhere");
        }
    }

    public EventDto AddEvent(AddEventDto newEvent)
    {
        var addNewEvent = _mapper.Map<Event>(newEvent);

        _repository.Event.AddEvent(addNewEvent);
        _repository.Save();

        var returnEvent = _mapper.Map<EventDto>(addNewEvent);
        return returnEvent;
    }


}

