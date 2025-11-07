using Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Payment.Request
{
    public class CreatePaymentRequest
    {
        [Required] public int TurnId { get; set; }
        [Required] public decimal Amount { get; set; }

        // No se piden DatePayment ni TimePayment porque se pueden obtener de la fecha y hora actuales al momento de crear el pago

        [Required] public MethodPayment Method { get; set; }
        [Required] public StatePayment State { get; set; }
    }
}
