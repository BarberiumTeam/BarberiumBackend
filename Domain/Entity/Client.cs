
using Domain.Entity;

namespace Domain
{
    public class Client : BaseEntity
    {
      public ICollection<Turn> Turns { get; set; } = new List<Turn>(); // Es una coleccion
      public string Name {  get; set; }
      public string Email { get; set; }
      public string Phone { get; set; }
      private string Password {  get; set; }
    }
}
