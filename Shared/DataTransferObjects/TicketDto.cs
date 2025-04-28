namespace Shared.DataTransferObjects;

    public record TicketDto(Guid Id, int TicketNumber, decimal Price,
                                        DateTime PurchaseTime, Guid EventId, string UserEmail);

