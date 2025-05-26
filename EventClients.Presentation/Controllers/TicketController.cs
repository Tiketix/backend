using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace EventClients.Presentation.Controllers
{
[Route("api/tickets")]
[ApiController]

    public class TicketController : ControllerBase
    {
        private readonly IServiceManager _service;
        public TicketController(IServiceManager service) => _service = service;


        [HttpGet]
        [Route("user-tickets")]
        public IActionResult GetTickets(string id)
        {
            try
            {
                var tickets =
                _service.TicketService.GetAllTickets(id, trackChanges: false);
                return Ok(tickets);
            }
            catch
            {
                return StatusCode(500, "Internal server errorrr");
            }
        }

        [HttpPost]
        [Route("add-ticket")]

        public IActionResult AddTicket([FromBody] AddTicketDto newTicket)
        {
            if (newTicket is null)
                return BadRequest("AddTicketDto object is null");

            var addNewTicket = _service.TicketService.AddTicket(newTicket);

            return Ok(addNewTicket);
        }

    }
}


