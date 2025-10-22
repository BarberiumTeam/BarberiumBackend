using Contracts.Client.Response;

namespace Application.Service
{
    public interface IClientService
    {
        ClientResponse GetClientById(int clientId);
    }
}