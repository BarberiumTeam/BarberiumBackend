using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstraction
{
    public interface IBarberRepository
    {
        // Esto son ReadOnly
        Barber? GetBarberById(int id);  // estps dos se manejan mas facil 
        List<Barber> GetAllBarbers();  // debido a que necesitan solo un response, porque devuelven lo mismo.

        // Esto son WriteOnly porque modifican y esperan una respuesta booleana
        bool CreateBarber(Barber barber);
        bool UpdateBarber(Barber barber);
        bool DeleteBarber(int id);
    }
}
