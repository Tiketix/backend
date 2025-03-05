using Microsoft.AspNetCore.Mvc;
using Service.Contracts;


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
    }
}