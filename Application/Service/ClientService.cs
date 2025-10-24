using Application.Abstraction;
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

        public ClientResponse GetClientById(int clientId)
        {
            var client = _clientRepository.GetClientById(clientId);

            return new ClientResponse
            {
                Name = client.Name,
                Email = client.Email,
                Phone = client.Phone,

            };

        }
    }
}
