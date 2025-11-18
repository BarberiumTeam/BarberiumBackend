namespace Domain.Entity
{
    public class BarberSchedule : BaseEntity
    {
        public int BarberId { get; set; } // FK
        public Barber? Barber { get; set; } // Propiedad navegacion
        public TimeOnly StartTime { get; set; }
        public WeekDay WeekDay { get; set; }
        public TimeOnly EndTime { get; set; }
    }
    public enum WeekDay
    {
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }
}
