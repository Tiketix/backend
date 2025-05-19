namespace Shared.DataTransferObjects
{
    public record EventDto(Guid Id, string EventOwner, string EventTitle, string EventDescription, 
                                    string OrganizerEmail, string Phone, string Location,
                                    int TicketsAvailable, DateOnly EventDate, string EventTime, double TicketPrice);

}
