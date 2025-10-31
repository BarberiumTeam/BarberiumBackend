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
        Barber? GetBarberById(int id);  
        List<Barber> GetAllBarbers();  

        // Esto son WriteOnly porque modifican y esperan una respuesta booleana
        bool CreateBarber(Barber barber);
        bool UpdateBarber(Barber barber);
        bool DeleteBarber(int id);
    }
}
