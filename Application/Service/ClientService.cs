using Application.Abstraction;
using Contracts.Client.Request;
using Contracts.Client.Response;

namespace Application.Service
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        // traer todos los clientes
        public List<ClientResponse> GetAllClients()
        {
            var clients = _clientRepository.GetAllClients();

            return clients.Select(client => new ClientResponse
            {
                Id = client.Id,
                Name = client.Name,
                Email = client.Email,
                Phone = client.Phone,
            }).ToList();
        }

        // trae un solo cliente por id
        public ClientResponse? GetClientById(int clientId)
        {
            var client = _clientRepository.GetClientById(clientId);

            if (client == null)
            {
                return null;
            }

            return new ClientResponse
            {
                Id = client.Id,
                Name = client.Name,
                Email = client.Email,
                Phone = client.Phone,

            };

        }
        
}
}
