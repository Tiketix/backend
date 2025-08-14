namespace Shared.DataTransferObjects
{
    public class AddTicketDto
    {
        public DateTime PurchaseTime { get; set; }
        public Guid EventId { get; set; }
        public required string UserId { get; set; }
        public required int NoOfTickets { get; set; }

    }
}


