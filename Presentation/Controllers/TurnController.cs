using Application.Service;
using Contracts.Turn.Request;
using Contracts.Turn.Response;
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
            return BadRequest($"No se pudo crear el turno. " +
                              $"\n\nVerifique 1) Que la hora de fin sea posterior a la hora de inicio y que la fecha sea de hoy en adelante" +
                              $"\nVerifique 2) Que el Cliente y Barbero existan." +
                              $"\nVerifique 3) Que el Barbero esté disponible (dentro de su horario y sin excepciones)." +
                              $"\nVerifique 4) Que el horario elegido no se solape con otro turno ya existente.");
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateTurnRequest request)
        {
            if (_turnService.UpdateTurn(id, request))
            {
                return NoContent();
            }
            return BadRequest($"No se pudo actualizar el turno con ID {id}. " +
                              $"\n\nVerifique 1) Que el Turno exista, que la hora de fin sea posterior a la hora de inicio y que la fecha sea de hoy en adelante." +
                              $"\nVerifique 2) Que el Barbero esté disponible en el nuevo horario (dentro de su horario y sin excepciones)." +
                              $"\nVerifique 3) Que el nuevo horario no se solape con otro turno ya existente del Barbero.");
        }

        [HttpPatch("{id}/state")]
        public IActionResult UpdateTurnState(int id, [FromBody] UpdateTurnStateRequest request)
        {
            if (_turnService.UpdateTurnState(id, request.NewState))
            {
                return NoContent();
            }
            return NotFound($"Turno con ID {id} no encontrado.");
        }

        [HttpPatch("{id}/service")]
        public IActionResult UpdateTurnServiceType(int id, [FromBody] UpdateTurnServiceTypeRequest request)
        {
            if (_turnService.UpdateTurnServiceType(id, request.NewServiceType))
            {
                return NoContent();
            }
            return NotFound($"Turno con ID {id} no encontrado u ocurrio algun error en actualizar.");
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
