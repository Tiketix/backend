namespace Shared.DataTransferObjects;

    public record TicketDto(Guid Id, decimal Price, DateTime PurchaseTime, 
                                            Guid EventId, string Email);

