using Domain.Entity;


namespace Application.Abstraction
{
    public interface IClientRepository
    {
        // Esto son ReadOnly
        Client? GetClientById(int id);  // estos dos se manejan mas facil 
        List<Client> GetAllClients();  // debido a que necesitan solo un response, porque devuelven lo mismo.

        // Esto son WriteOnly porque modifican y esperan una respuesta booleana
        bool UpdateClient(Client client);
        bool DeleteClient(int id);
    }
}
