namespace Domain.Entity
{
    public class Payment : BaseEntity
    {
        public int TurnId { get; set; } // FK
        public Turn? Turn { get; set; } // Propiedad navegacion
        public decimal Amount { get; set; }
        public DateOnly DatePayment { get; set; }
        public TimeOnly TimePayment { get; set; }
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
