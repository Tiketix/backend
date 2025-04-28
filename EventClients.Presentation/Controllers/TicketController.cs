using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace EventClients.Presentation.Controllers
{
[Route("api/tickets")]
[ApiController]

    public class TicketController : ControllerBase
    {
        private readonly IServiceManager _service;
        public TicketController(IServiceManager service) => _service = service;


        [HttpGet]
        public IActionResult GetTickets(string email)
        {
            try
            {
                var tickets =
                _service.TicketService.GetAllTickets(email, trackChanges: false);
                return Ok(tickets);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}


