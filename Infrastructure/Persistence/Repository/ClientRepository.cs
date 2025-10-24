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

    public async Task<Client?> CreateClientAsync(Client client)
    {
        _context.Clients.Add(client);
        await _context.SaveChangesAsync();

        return client;
    }

    public Client? GetClientById(int id)
    {
        return _context.Clients.FirstOrDefault(i => i.Id == id);
    }

}
