namespace Shared.DataTransferObjects
{
    // DTO for ticket operations.
    public class TicketDto
    {
        public int UserId { get; set; }    // ID of the user booking the ticket
        public int EventId { get; set; }   // ID of the event for which the ticket is booked
        public decimal Price { get; set; } // Price of the ticket
    }
}
