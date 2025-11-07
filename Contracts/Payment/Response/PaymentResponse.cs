using Domain.Entity;

namespace Contracts.Payment.Response
{
    public class PaymentResponse
    {
        public int Id { get; set; }
        public int TurnId { get; set; }
        public decimal Amount { get; set; }

    
        public DateOnly DatePayment { get; set; }
        public TimeOnly TimePayment { get; set; }

        public MethodPayment Method { get; set; }
        public StatePayment State { get; set; }
    }
}
