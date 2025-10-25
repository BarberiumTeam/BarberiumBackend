using Contracts.Client.Request;
using Contracts.Client.Response;

namespace Application.Service
{
    public interface IClientService
    {
        List<ClientResponse> GetAllClients();
        ClientResponse? GetClientById(int clientId);

        bool CreateClient(CreateClientRequest request);
        bool UpdateClient(int id, UpdateClientRequest request);
    }
}