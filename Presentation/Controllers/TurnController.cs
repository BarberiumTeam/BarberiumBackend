using Application.Service;
using Contracts.Turn.Request;
using Contracts.Turn.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TurnController : ControllerBase
    {
        // ⭐️ CORREGIDO: Inyección de la interfaz de SERVICIO
        private readonly ITurnService _turnService;

        // ⭐️ CORREGIDO: Constructor recibe ITurnService
        public TurnController(ITurnService turnService)
        {
            _turnService = turnService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TurnResponse>> GetAll()
        {
            return Ok(_turnService.GetAllTurns());
        }

        [HttpGet("{id}")]
        public ActionResult<TurnResponse?> GetById(int id)
        {
            var turn = _turnService.GetTurnById(id);
            return turn == null ? NotFound() : Ok(turn);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateTurnRequest request)
        {
            if (_turnService.CreateTurn(request))
            {
                return Created();
            }
            return BadRequest("No se pudo crear el turno. Revise los datos.");
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateTurnRequest request)
        {
            if (_turnService.UpdateTurn(id, request))
            {
                return NoContent();
            }
            return NotFound($"Turno con ID {id} no encontrado o datos inválidos.");
        }

        [HttpPatch("{id}/state")]
        public IActionResult UpdateTurnState(int id, [FromBody] UpdateTurnStateRequest request)
        {
            if (_turnService.UpdateTurnState(id, request.NewState))
            {
                return NoContent();
            }
            return BadRequest($"No se pudo actualizar el estado del turno {id}.");
        }

        [HttpPatch("{id}/service")]
        public IActionResult UpdateTurnServiceType(int id, [FromBody] UpdateTurnServiceTypeRequest request)
        {
            if (_turnService.UpdateTurnServiceType(id, request.NewServiceType))
            {
                return NoContent();
            }
            return BadRequest($"No se pudo actualizar el tipo de servicio del turno {id}.");
        }

        [HttpDelete("{id}")]
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
