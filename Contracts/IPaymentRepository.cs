using Entities.Models;

namespace Contracts
{
    // Defines data access methods for payment operations.
    public interface IPaymentRepository
    {
        Task AddPaymentAsync(Payment payment);
        Task SaveChangesAsync();
        Task<Payment> GetPaymentByIdAsync(int paymentId);
        Task<IEnumerable<Payment>> GetPaymentsByUserAsync(int userId);
    }
}
