namespace Shared.DataTransferObjects
{
    public record AddEventDto(string EventTitle, string EventDescription, 
                                    string OrganizerEmail, string Phone, string Location,
                                    int TicketsAvailable, DateOnly EventDate, string EventTime);

}
