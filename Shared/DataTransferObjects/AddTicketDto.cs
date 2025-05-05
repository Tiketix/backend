namespace Shared.DataTransferObjects;

public record AddTicketDto(decimal Price, DateTime PurchaseTime, 
                                Guid EventId, string UserId);


