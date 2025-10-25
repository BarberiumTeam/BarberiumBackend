using Domain.Entity;


namespace Application.Abstraction
{
    public interface IClientRepository
    {
        Client? GetClientById(int id);  // estps dos se manejan mas facil 
        List<Client> GetAllClients();  // debido a que necesitan solo un response, porque devuelven lo mismo.

    }
}
