using Domain.Entity;


namespace Application.Abstraction
{
    public interface IClientRepository
    {
        Client? GetClientById(int id);
        Task<Client?> CreateClientAsync(Client client);
    
    }
}
