using Contracts.Client.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public class ClientService : IClientService
    {
        public ClientResponse GetClientById(int clientId)
        {
            // Aquí deberías implementar la lógica real para obtener el cliente
            // Por ahora, retornamos un cliente de prueba
            return new ClientResponse
            {
                Id = 1,
                Name = "Cliente de prueba",
                Email = "cliente@ejemplo.com"
            };
        }
    }
}
