namespace Shared.DataTransferObjects
{
    public record UpdateEventDetailsDto(string EventTitle, string EventDescription, 
                                string Location, DateOnly EventDate, string EventTime);

}