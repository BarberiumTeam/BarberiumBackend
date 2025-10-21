
namespace Domain.Entities
{
    public class Payment : BaseEntity
    {
        public int TurnId { get; set; } // FK
        public Turn Turn { get; set; } // Propiedad navegacion
        public Decimal Amount { get; set; }
        public DateTime DatePayment { get; set; }
        public MethodPayment Method { get; set; }
        public StatePayment State { get; set; }

    }
    public enum MethodPayment
    {
        Effective,
        DebitCard,
        VirtualWallet
    }
    public enum StatePayment
    {
        Pending,
        Canceled,
        Paid,
        Failed,
        Refunded
    }
}
