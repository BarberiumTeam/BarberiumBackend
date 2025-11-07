using Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace Contracts.Payment.Request
{
    public class UpdatePaymentRequest
    {
        [Required] public int TurnId { get; set; }
        [Required] public decimal Amount { get; set; }
        [Required] public MethodPayment Method { get; set; }
        [Required] public StatePayment State { get; set; }
    }
}
