namespace Shared.DataTransferObjects
{
    public record EventDto(Guid Id, string EventTitle, string EventDescription, 
                                    string OrganizerEmail, string Phone, string Location,
                                    int TicketsAvailable, DateOnly EventDate, string EventTime);

}
