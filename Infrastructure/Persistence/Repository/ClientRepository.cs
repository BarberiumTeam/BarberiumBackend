using Application.Abstraction;
using Domain.Entity;
using System.Linq;

namespace Infrastructure.Persistence.Repository;

public class ClientRepository : IClientRepository
{
    private readonly BarberiumDbContext _context;
    public ClientRepository(BarberiumDbContext context)
    {
        _context = context;
    }

    public List<Client> GetAllClients()
    {
        return _context.Clients.ToList();
    }

    public Client? GetClientById(int id)
    {
        return _context.Clients.FirstOrDefault(i => i.Id == id);
    }

}
