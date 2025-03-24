using Contracts;
using Entities.Models;
using Repository;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    // Implements payment data access using EF Core.
    public class PaymentRepository : IPaymentRepository
    {
        private readonly RepositoryContext _context;
        
        public PaymentRepository(RepositoryContext context)
        {
            _context = context;
        }
        
        public async Task AddPaymentAsync(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
        }
        
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        
        public async Task<Payment> GetPaymentByIdAsync(int paymentId)
        {
            var payment = await _context.Payments.FindAsync(paymentId);
            if (payment == null)
            {
                throw new KeyNotFoundException($"Payment with ID {paymentId} was not found.");
            }
            return payment;
        }
        
        public async Task<IEnumerable<Payment>> GetPaymentsByUserAsync(int userId)
        {
            return await _context.Payments.Where(p => p.UserId == userId).ToListAsync();
        }
    }
}
