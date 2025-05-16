namespace Shared.DataTransferObjects;

    public record TicketDto(Guid TicketId, decimal Price, DateTime PurchaseTime,
                                string EventTitle, string OrganizerEmail, string LastName, Guid EventId, string UserId); 

