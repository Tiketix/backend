using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;


namespace EventClients.Presentation.Controllers
{
[Route("api/events")]
[ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IServiceManager _service;
        public EventsController(IServiceManager service) => _service = service;


        [HttpGet]
        public IActionResult GetEvents()
        {
            try
            {
                var events = 
                _service.EventService.GetAllEvents(trackChanges: false);
                return Ok(events);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("events-by-time")]
        public IActionResult GetEventsByTime(string time)
        {
            try
            {
                var events = 
                _service.EventService.GetEventsByTime(time, trackChanges: false);
                return Ok(events);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("events-by-title")]

        public IActionResult GetEventByTitle(string title)
        {
            try
            {
                var eventResult = _service.EventService.GetEventByTitle(title, trackChanges: false);

                if (eventResult is null)
                {
                    return NotFound("Event does not exist on this website!!");
                }
                return Ok(eventResult);
            }
            catch
            {
                return StatusCode(500, "Something is wrong somewhere.");
            }
        }

        [HttpPost]
        [Route("add-event")]

        public IActionResult AddEvent([FromBody] AddEventDto newEvent)
        {
            if (newEvent is null)
                    return BadRequest("AddEventDto object is null");

            var addNewEvent = _service.EventService.AddEvent(newEvent);

            return Ok(addNewEvent);
        }


    }
}