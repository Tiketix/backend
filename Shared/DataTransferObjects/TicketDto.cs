namespace Shared.DataTransferObjects;

    public record TicketDto(Guid TicketId, decimal Price, DateTime PurchaseTime,
                                string EventTitle, Guid EventId, string UserId); 

