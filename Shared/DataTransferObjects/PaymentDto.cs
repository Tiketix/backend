namespace Shared.DataTransferObjects
{
    // DTO for payment processing.
    public class PaymentDto
    {
        public int UserId { get; set; }    // ID of the user making the payment
        public int TicketId { get; set; }  // ID of the ticket for which payment is made
        public decimal Amount { get; set; } // Payment amount
    }
}
