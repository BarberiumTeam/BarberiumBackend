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


        [HttpPut("{id}")]
        public IActionResult UpdateClient(int id, [FromBody] UpdateClientRequest request)
        {
            if (id <= 0)
                return BadRequest("Id invalido");

            if (request == null)
                return BadRequest("Datos inválidos");

            // 💾 Intentar actualizar (el service ya valida todo)
            bool success = _clientService.UpdateClient(id, request);

            if (!success)
            {
                // 🔎 Verificar si existe para diferenciar error
                var client = _clientService.GetClientById(id);

                if (client == null)
                    return NotFound($"Cliente con {id} no encontrado");

                return BadRequest("El email ya está registrado.");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteClient(int id)
        {
            if (id <= 0) return BadRequest("Id invalido");

            bool success = _clientService.DeleteClient(id);

            if (!success)
            {
                return NotFound($"Cliente con el {id} no fue encontrado y no se pudo borrar");
            }

            return NoContent();
        }
        
    }
}
