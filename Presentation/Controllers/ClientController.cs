using Application.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Contracts.Client.Response;
using Contracts.Client.Request;

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

        [HttpPost]
        public IActionResult CreateClient([FromBody] CreateClientRequest request)
        {
            bool success = _clientService.CreateClient(request);

            if (!success)
            {
                return BadRequest("No se creo el cliente, revise nuevamente los datos");
            }

            return Created();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateClient(int id, [FromBody] UpdateClientRequest request)
        {
            if (id <= 0) return BadRequest("Id invalido");

            bool success = _clientService.UpdateClient(id, request);

            if (!success)
            {
                return NotFound($"Cliente con {id} no encontrado o no se pudo actualizar");
            }

            return NoContent();
        }
        
    }
}
