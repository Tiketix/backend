using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;


namespace EventClients.Presentation.Controllers
{
    // API Controller for payment operations.
    [Route("api/payments")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        
        // Processes a payment.
        [HttpPost("process")]
        public async Task<IActionResult> ProcessPayment([FromBody] PaymentDto paymentDto)
        {
            var payment = await _paymentService.ProcessPaymentAsync(paymentDto);
            return Ok(payment);
        }
        
        // Retrieves a payment by its ID.
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPayment(int id)
        {
            var payment = await _paymentService.GetPaymentByIdAsync(id);
            if (payment == null) return NotFound();
            return Ok(payment);
        }
        
        // Refunds a payment.
        [HttpPost("{id}/refund")]
        public async Task<IActionResult> RefundPayment(int id)
        {
            var result = await _paymentService.RefundPaymentAsync(id);
            if (!result) return NotFound(new { message = "Payment not found or refund failed" });
            return Ok(new { message = "Payment refunded successfully" });
        }
    }
}
