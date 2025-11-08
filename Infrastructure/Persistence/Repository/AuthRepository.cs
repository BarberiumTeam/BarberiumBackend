using Application.Abstraction;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly BarberiumDbContext _context;

        public AuthRepository(BarberiumDbContext context) => _context = context;

        public object? GetUserByEmail(string email)
        {
            var client = _context.Clients.FirstOrDefault(c => c.Email == email);
            if (client != null) return client;

            var barber = _context.Barbers.FirstOrDefault(b => b.Email == email);
            return barber;
        }

        public bool EmailExists(string email)
        {
            return _context.Clients.Any(c => c.Email == email) ||
                   _context.Barbers.Any(b => b.Email == email);
        }

        public bool AddClient(Client client)
        {
            _context.Clients.Add(client);
            // En un proyecto real se usa SaveChangesAsync()
            return _context.SaveChanges() > 0;
        }

        public bool AddBarber(Barber barber)
        {
            _context.Barbers.Add(barber);
            return _context.SaveChanges() > 0;
        }
    }
}
