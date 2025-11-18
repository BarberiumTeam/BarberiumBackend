namespace Domain.Entity
{
    public class Turn : BaseEntity
    {
        public int ClientId { get; set; } // FK
        public int BarberId { get; set; } // FK

        public Client? Client { get; set; } //Propiedad navegacion
        public Barber? Barber {  get; set; } //Propiedad navegacion
        public Payment? Payment { get; set; } //Propiedad navegacion

        public ServiceType Service { get; set; }
        public DateOnly TurnDate { get; set; }
        public TimeOnly TurnStartTime { get; set; }
        public TimeOnly TurnEndTime { get; set; }
        public State State { get; set; }

    }

    public enum ServiceType
    {
        Haircut,
        Beard,
        HaircutAndBeard,
        Shaving,
        Washing
    }
    public enum State
    {
        Scheduled,
        Completed,
        Cancelled,
        NoShow
    }
}
