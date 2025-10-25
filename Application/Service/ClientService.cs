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

        public Task<ClientResponse> CreateClientAsync(ClientRequest request, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

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
