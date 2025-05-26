namespace Shared.DataTransferObjects;

public record AddTicketDto(DateTime PurchaseTime,
                                Guid EventId, string UserId);


