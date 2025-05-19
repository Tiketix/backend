namespace Shared.DataTransferObjects;

    public record TicketDto(Guid TicketId, double TicketPrice, DateTime PurchaseTime,
                                string EventTitle, string OrganizerEmail, string LastName,
                                string FirstName, Guid EventId, string UserId); 

