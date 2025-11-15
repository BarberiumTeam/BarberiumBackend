using Application.Service;
using Contracts.Turn.Request;
using Contracts.Turn.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TurnController : ControllerBase
    {
        private readonly ITurnService _turnService;

        public TurnController(ITurnService turnService)
        {
            _turnService = turnService;
        }

        [HttpGet]
        [Authorize(Roles = "Barber")]
        public ActionResult<IEnumerable<TurnResponse>> GetAll()
        {
            return Ok(_turnService.GetAllTurns());
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Barber,Client")]  // se usa una coma para separar multiples roles
        public ActionResult<TurnResponse?> GetById(int id)
        {
            var turn = _turnService.GetTurnById(id);
            return turn == null ? NotFound() : Ok(turn);
        }

        [HttpPost]
        [Authorize(Roles = "Client")]
        public IActionResult Create([FromBody] CreateTurnRequest request)
        {
            if (_turnService.CreateTurn(request))
            {
                return Created();
            }
            return BadRequest("No se pudo crear el turno. Verifique que el cliente, el barbero existan y que el horario esté disponible.");
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Client")]
        public IActionResult Update(int id, [FromBody] UpdateTurnRequest request)
        {
            if (_turnService.UpdateTurn(id, request))
            {
                return NoContent();
            }
            return BadRequest("No se pudo actualizar el turno. Verifique que el horario sea válido o no se solape con otro turno");
        }

        [HttpPatch("{id}/state")]
        [Authorize(Roles = "Barber")]
        public IActionResult UpdateTurnState(int id, [FromBody] UpdateTurnStateRequest request)
        {
            if (_turnService.UpdateTurnState(id, request.NewState))
            {
                return NoContent();
            }
            return NotFound($"Turno con ID {id} no encontrado.");
        }

        [HttpPatch("{id}/service")]
        [Authorize(Roles = "Client")]
        public IActionResult UpdateTurnServiceType(int id, [FromBody] UpdateTurnServiceTypeRequest request)
        {
            if (_turnService.UpdateTurnServiceType(id, request.NewServiceType))
            {
                return NoContent();
            }
            return NotFound($"Turno con ID {id} no encontrado.");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Client")]
        public IActionResult Delete(int id)
        {
            if (_turnService.DeleteTurn(id))
            {
                return NoContent();
            }
            return NotFound($"Turno con ID {id} no encontrado.");
        }
    }
}
