namespace Domain.Entity
{
    public class Client : BaseEntity
    {
        public ICollection<Turn> Turns { get; set; } = new List<Turn>();

        //  PROPIEDADES DE AUTENTICACIÓN
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty; //  USADO PARA LOGIN
        public string PasswordHash { get; set; } = string.Empty; //  USADO PARA BCrypt
        public string Role { get; set; } = "Client"; //  ROL FIJO PARA AUTORIZACIÓN

        public string Phone { get; set; } = string.Empty;
    }
}
