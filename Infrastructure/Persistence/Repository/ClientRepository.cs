using Application.Abstraction;
using Domain.Entity;

namespace Infrastructure.Persistence.Repository;

public class ClientRepository : IClientRepository
{
    private readonly BarberiumDbContext _context;
    public ClientRepository(BarberiumDbContext context)
    {
        _context = context;
    }

    public Client GetClientById(int id)
    {
        throw new NotImplementedException();
    }
}
