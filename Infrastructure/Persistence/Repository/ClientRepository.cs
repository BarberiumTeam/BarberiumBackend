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

    public bool CreateClient(Client client)
    {
        _context.Clients.Add(client);
        return _context.SaveChanges() > 0;
    }

    public bool UpdateClient(Client client)
    {
        _context.Clients.Update(client);
        return _context.SaveChanges() > 0;
    }

}
