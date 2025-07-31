namespace Shared.DataTransferObjects
{
    public class UpdateEventDetailsDto
    {
        public string? EventTitle { get; set; }
        public string? EventDescription { get; set; }
        public string? Location { get; set; }
        public DateOnly? EventDate { get; set; }
        public string? EventTime { get; set; }
        public string? PhoneNo { get; set; }
        public double? TicketPrice { get; set; }
        public int? TicketsAvailable { get; set; }

        
    }

}