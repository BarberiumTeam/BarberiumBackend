using Application.Service;
using Contracts.Payment.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        public IActionResult GetAllPayments()
        {
            var payments = _paymentService.GetAllPayments();
            return Ok(payments);
        }

        [HttpGet("{id}")]
        public IActionResult GetPaymentById(int id)
        {
            var payment = _paymentService.GetPaymentById(id);
            if (payment == null)
            {
                return NotFound();
            }
            return Ok(payment);
        }

        [HttpPost]
        public IActionResult CreatePayment([FromBody] CreatePaymentRequest request)
        {
            var result = _paymentService.CreatePayment(request);
            if (!result)
            {
                return BadRequest($"No se pudo registrar el pago. " +
                                      $"\n\nVerifique 1) Si el Turno con ID {request.TurnId} existe." +
                                      $"\nVerifique 2) Si el Pago para ese Turno aún no ha sido creado.");
            }
            return Created();
            
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdatePaymentRequest request)
        {
            if (_paymentService.UpdatePayment(id, request))
            {
                return NoContent();
            }
            return NotFound($"Pago con ID {id} no encontrado.");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_paymentService.DeletePayment(id))
            {
                return NoContent();
            }
            return NotFound($"Pago con ID {id} no encontrado.");
        }

    }
}
