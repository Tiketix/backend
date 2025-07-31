using Entities.Response;
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


        [HttpGet("getEvents")]
        public async Task<IActionResult> GetEvents()
        { 
            var response = await _service.EventService.GetAllEvents(trackChanges: false);
            
            if (!response.Success)
                return NotFound(response);
            return Ok(response);
        }

        [HttpGet]
        [Route("eventsByTime")]
        public async Task<IActionResult> GetEventsByTime(string time)
        {
            var response = await _service.EventService.GetEventsByTime(time, trackChanges: false);
            if (!response.Success)
                return NotFound(response);
            return Ok(response);
        }

        [HttpGet("eventsByTitle")]
        public async Task<IActionResult> GetEventByTitle(string title)
        {
            var response = await _service.EventService.GetEventByTitle(title, trackChanges: false);
            if (!response.Success)
                return NotFound(response);
            return Ok(response);
        }

        [HttpPost]
        [Route("add-event")]
        public async Task<IActionResult> AddEvent([FromBody] AddEventDto newEvent)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(ApiResponse<string>.FailureResponse(errors, "Invalid request data!"));
            }
            try
            {
                var response = await _service.EventService.AddEvent(newEvent);
                if (!response.Success)
                    return BadRequest(response);

                return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("updateEventDetails")]
        public async Task<IActionResult> UpdateEventDetails(Guid id, [FromBody] UpdateEventDetailsDto newEventDetails)
        {
            var response = await _service.EventService.UpdateEventDetails(id, newEventDetails, trackChanges: true);
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpDelete("deleteEvent")]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            var response = await _service.EventService.DeleteEvent(id, trackChanges: false);
            if (!response.Success)
                return BadRequest(response);

            return Ok("Event Deleted successfully");
        }


    }
}