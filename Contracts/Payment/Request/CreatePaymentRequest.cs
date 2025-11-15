using Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace Contracts.Payment.Request
{
    public class CreatePaymentRequest
    {
        // TurnId (ID del Turno asociado)
        [Required(ErrorMessage = "El ID del turno es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del turno debe ser un número positivo (mínimo 1).")]
        public int TurnId { get; set; }

        // Amount (Monto del pago)
        [Required(ErrorMessage = "El monto del pago es obligatorio.")]
        // Aseguramos que el monto sea positivo y mayor a cero.
        [Range(0.01, (double)decimal.MaxValue, ErrorMessage = "El monto debe ser positivo y mayor a cero.")]
        public decimal Amount { get; set; }

        // Method (Método de Pago - Enum)
        // 1. Requerido. 2. Debe ser un miembro válido del enum.
        [Required(ErrorMessage = "Debe especificar un método de pago.")]
        [EnumDataType(typeof(MethodPayment), ErrorMessage = "El método de pago proporcionado no es válido.")]
        public MethodPayment Method { get; set; }

        // State (Estado del Pago - Enum)
        // 1. Requerido. 2. Debe ser un miembro válido del enum.
        [Required(ErrorMessage = "Debe especificar un estado de pago.")]
        [EnumDataType(typeof(StatePayment), ErrorMessage = "El estado de pago proporcionado no es válido.")]
        public StatePayment State { get; set; }
    }
}