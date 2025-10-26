using Application.Abstraction;
using Contracts.Client.Request;
using Contracts.Client.Response;
using Domain.Entity;

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

        public bool CreateClient(CreateClientRequest request)
        {
            // Aca iria la logica de negocio ej: Si el email existe

            // Esto es el mapeo del DTO a entidad dominio
            var ClientEntity = new Client
            {
                Name = request.Name,
                Email = request.Phone,
                Phone = request.Phone,

                //Aca se hashea la contraseña.
            };
            return _clientRepository.CreateClient(ClientEntity);
        }

        public bool UpdateClient(int id, UpdateClientRequest request)
        {
            var ClientToUpdate = _clientRepository.GetClientById(id);

            if (ClientToUpdate == null)
            {
                return false;
            }
            ClientToUpdate.Name = request.Name;
            ClientToUpdate.Email = request.Email;
            ClientToUpdate.Phone = request.Phone;

            return _clientRepository.UpdateClient(ClientToUpdate);
        }

        public bool DeleteClient(int id)
        {
            return _clientRepository.DeleteClient(id); 
        }
    }
}
