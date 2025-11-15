using Contracts.Client.Request;
using Contracts.Client.Response;

namespace Application.Service
{
    public interface IClientService
    {
        List<ClientResponse> GetAllClients();
        ClientResponse? GetClientById(int clientId);
        bool UpdateClient(int id, UpdateClientRequest request);
        bool DeleteClient(int id);
    }
}