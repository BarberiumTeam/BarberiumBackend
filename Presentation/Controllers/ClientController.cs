using Application.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Contracts.Client.Response;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ClientResponse>> GetAllClients()
        {
            var clients = _clientService.GetAllClients();
            return Ok(clients);
        }

        [HttpGet("{clientId}")]
        public ActionResult<ClientResponse?> GetClientById(int clientId)
        {
            var client = _clientService.GetClientById(clientId);

            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        

    }
}
