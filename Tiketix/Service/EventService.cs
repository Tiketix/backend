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
            throw new Exception($"No Events takes place at {time}");
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

    public void DeleteEvent(string title, bool trackchanges)
    {
        var eventName = _repository.Event.GetEventByTitle(title, trackchanges) ?? throw new Exception("event does not exist in database");

        _repository.Event.DeleteEvent(eventName);
        _repository.Save();
    }

    public void UpdateEventDetails(string title, UpdateEventDetailsDto updateEventDetails, bool trackchanges)
    {
        var eventName = _repository.Event.GetEventByTitle(title, trackchanges) ?? throw new Exception("event does not exist in database");
        
        _mapper.Map(updateEventDetails, eventName);
        _repository.Save();
    }


}

