using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http; // Add this line
using Service.Contracts;
using Shared.DataTransferObjects;


namespace EventClients.Presentation.Controllers
{
    /// <summary>
    /// API Controller for ticket operations.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        
        /// <summary>
        /// Initializes a new instance of the TicketController.
        /// </summary>
        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }
        
        /// <summary>
        /// Books a new ticket.
        /// </summary>
        [HttpPost("book")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TicketDto>> BookTicket([FromBody] TicketDto ticketDto)
        {
            var ticket = await _ticketService.BookTicketAsync(ticketDto);
            return Ok(ticket);
        }
        
        /// <summary>
        /// Retrieves a ticket by its ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicketById(int id)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(id);
            if (ticket == null) return NotFound();
            return Ok(ticket);
        }
        
        /// <summary>
        /// Retrieves tickets booked by a specific user.
        /// </summary>
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetTicketsByUser(int userId)
        {
            var tickets = await _ticketService.GetTicketsByUserAsync(userId);
            return Ok(tickets);
        }
        
        /// <summary>
        /// Retrieves tickets for a specific event.
        /// </summary>
        [HttpGet("event/{eventId}")]
        public async Task<IActionResult> GetTicketsByEvent(int eventId)
        {
            var tickets = await _ticketService.GetTicketsByEventAsync(eventId);
            return Ok(tickets);
        }
        
        /// <summary>
        /// Cancels a ticket.
        /// </summary>
        [HttpDelete("{id}/cancel")]
        public async Task<IActionResult> CancelTicket(int id)
        {
            var result = await _ticketService.CancelTicketAsync(id);
            if (!result) return NotFound(); 
            return Ok(new { message = "Ticket cancelled successfully" });
        }
    }
}
