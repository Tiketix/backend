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
        [Route("userTickets")]
        public async Task<IActionResult> GetTickets([FromQuery] GetTicketRequest request)
        {
            var response = await _service.TicketService.GetAllUserTickets(request, trackChanges: false);
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);  
        }

        [HttpPost]
        [Route("addTicket")]

        public async Task<IActionResult> AddTicket([FromBody] AddTicketDto newTicket)
        {
            var response = await _service.TicketService.AddTicket(newTicket);
            if (!response.Success)
                return BadRequest(response);

            return Ok(response); 

        }

    }
}


