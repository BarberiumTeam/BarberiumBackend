namespace Domain.Entity
{
    public class Barber : BaseEntity
    {
        public ICollection<Turn> Turns { get; set; } = new List<Turn>();
        public ICollection<BarberSchedule> Schedules { get; set; } = new List<BarberSchedule>();
        public ICollection<ScheduleException> Exceptions { get; set; } = new List<ScheduleException>();

        // PROPIEDADES DE AUTENTICACIÓN
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty; //  USADO PARA LOGIN
        public string PasswordHash { get; set; } = string.Empty; //  USADO PARA BCrypt
        public string Role { get; set; } = "Barber"; //  ROL FIJO PARA AUTORIZACIÓN

        public string Phone { get; set; } = string.Empty;
    }
}
