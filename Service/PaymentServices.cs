using AutoMapper;
using Contracts;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service
{
    // Implements business logic for payment operations.
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        
        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository ?? throw new ArgumentNullException(nameof(paymentRepository));
        }
        
        // Processes a payment based on the provided DTO.
        public async Task<Payment> ProcessPaymentAsync(PaymentDto paymentDto)
        {
            var payment = new Payment
            {
                UserId = paymentDto.UserId,
                TicketId = paymentDto.TicketId,
                Amount = paymentDto.Amount,
                PaymentDate = DateTime.UtcNow,
                Status = "Processed"
            };
            await _paymentRepository.AddPaymentAsync(payment);
            await _paymentRepository.SaveChangesAsync();
            return payment;
        }
        
        // Retrieves a payment by its ID.
        public async Task<Payment> GetPaymentByIdAsync(int paymentId)
        {
            return await _paymentRepository.GetPaymentByIdAsync(paymentId);
        }
        
        // Retrieves all payments for a specific user.
        public async Task<IEnumerable<Payment>> GetPaymentsByUserAsync(int userId)
        {
            return await _paymentRepository.GetPaymentsByUserAsync(userId);
        }
        
        // Refunds a payment by updating its status to "Refunded".
        public async Task<bool> RefundPaymentAsync(int paymentId)
        {
            var payment = await _paymentRepository.GetPaymentByIdAsync(paymentId);
            if (payment == null) return false;
            payment.Status = "Refunded";
            await _paymentRepository.SaveChangesAsync();
            return true;
        }

        Task<PaymentDto> IPaymentService.ProcessPaymentAsync(PaymentDto paymentDto)
        {
            throw new NotImplementedException();
        }

        Task<PaymentDto> IPaymentService.GetPaymentByIdAsync(int paymentId)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<PaymentDto>> IPaymentService.GetPaymentsByUserAsync(int userId)
        {
            throw new NotImplementedException();
        }
    }
}