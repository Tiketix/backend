using Shared.DataTransferObjects;

namespace Service.Contracts
{
    // Defines business logic methods for payment operations.
    public interface IPaymentService
    {
        // Processes a payment using the provided DTO.
        Task<PaymentDto> ProcessPaymentAsync(PaymentDto paymentDto);
        
        // Retrieves a payment by its ID.
        Task<PaymentDto> GetPaymentByIdAsync(int paymentId);
        
        // Retrieves all payments for a specific user.
        Task<IEnumerable<PaymentDto>> GetPaymentsByUserAsync(int userId);
        
        // Refunds a payment by its ID.
        Task<bool> RefundPaymentAsync(int paymentId);
    }
}
