namespace Shared.DataTransferObjects
{
    public class TicketDto
    {
        public Guid TicketId { get; set; }
        public double TicketPrice { get; set; }
        public DateTime PurchaseTime { get; set; }
        public string? EventTitle { get; set; }
        public string? OrganizerEmail { get; set; }
        public string? PurchaserLastName { get; set; }
        public string? PurchaserFirstName { get; set; }
        public Guid EventId { get; set; }
        public string? UserId { get; set; }
    }

    // public class TicketOrderDto
    // {
    //     public Guid TicketId { get; set; }
    //     public double TicketPrice { get; set; }
    //     public DateTime PurchaseTime { get; set; }
    //     public string? EventTitle { get; set; }
    //     public string? OrganizerEmail { get; set; }
    //     public string? PurchaserLastName { get; set; }
    //     public string? PurchaserFirstName { get; set; }
    //     public Guid EventId { get; set; }
    //     public int NoOfTicketsOrdered { get; set; }
    //     public string? UserId { get; set; }
    // }

    public class GetTicketRequest
    {
        public required string Id { get; set; }
    }
    public class UserTicketsDto
    {
        public IEnumerable<EventTicketCountDto>? TicketCountByEvent { get; set; }
        public IEnumerable<TicketDto>? Tickets { get; set; }

    }

    public class EventTicketCountDto
    {
        public Guid EventId { get; set; }
        public string? EventName { get; set; }
        public int TicketCount { get; set; }
    }
}

