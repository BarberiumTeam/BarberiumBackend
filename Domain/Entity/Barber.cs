namespace Domain.Entity
{
    public class Barber : BaseEntity
    {
        public ICollection<Turn> Turns { get; set; } = new List<Turn>(); // Coleccion
        public ICollection<BarberSchedule> Schedules { get; set; } = new List<BarberSchedule>(); // Coleccion
        public ICollection<ScheduleException> Exceptions { get; set; } = new List<ScheduleException>(); // Coleccion
        public string Name { get; set; }
        public string Specialty { get; set; }
        private string Password { get; set; }
    }
}
