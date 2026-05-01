using Application.Abstraction;
using Domain.Entity;
using System.Linq;

namespace Infrastructure.Persistence.Repository
{
    public class BarberRepository : IBarberRepository
    {
        private readonly BarberiumDbContext _context;

        public BarberRepository(BarberiumDbContext context)
        {
            _context = context;
        }
        public List<Barber> GetAllBarbers()
        {
            return _context.Barbers.ToList();
        }

        public Barber? GetBarberById(int id)
        {
            return _context.Barbers.FirstOrDefault(i => i.Id == id);
        }


        public bool UpdateBarber(Barber barber)
        {
            _context.Barbers.Update(barber);
            return _context.SaveChanges() > 0;
        }

        public bool DeleteBarber(int id)
        {
            var BarberToDelete = _context.Barbers.FirstOrDefault(c => c.Id == id);

            if (BarberToDelete == null) return false;

            _context.Barbers.Remove(BarberToDelete);

            return _context.SaveChanges() > 0;
        }

        public bool EmailExists(string email, int? excludeBarberId = null)
        {
            var existsInClients = _context.Clients.Any(c => c.Email == email);

            var existsInBarbers = _context.Barbers.Any(b =>
                b.Email == email &&
                (excludeBarberId == null || b.Id != excludeBarberId)
    );

            return existsInClients || existsInBarbers;
        }
        public bool HasAnyBarber()
        {
            return _context.Barbers.Any();
        }
    }
}
