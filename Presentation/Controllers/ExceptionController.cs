using Application.Service;
using Contracts.ScheduleException.Response;
using Contracts.ScheduleException.Request;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExceptionController : ControllerBase
    {
        private readonly IScheduleExceptionService _scheduleExceptionService;

        public ExceptionController(IScheduleExceptionService scheduleExceptionService)
        {
            _scheduleExceptionService = scheduleExceptionService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ScheduleExceptionResponse>> GetAllScheduleExceptions()
        {
            var scheduleExceptions = _scheduleExceptionService.GetAllScheduleExceptions();
            return Ok(scheduleExceptions);
        }

        [HttpGet("{id}")]
        public ActionResult<ScheduleExceptionResponse?> GetScheduleExceptionById(int id)
        {
            var scheduleException = _scheduleExceptionService.GetScheduleExceptionById(id);
            if (scheduleException == null)
            {
                return NotFound();
            }
            return Ok(scheduleException);
        }

        [HttpPost]
        public IActionResult CreateScheduleException([FromBody] CreateScheduleExceptionRequest request)
        {
            if (_scheduleExceptionService.CreateScheduleException(request))
            {
                return Created();
            }

            return BadRequest("No se pudo crear la excepción de horario. " +
                             "\n\nVerifique 1) Que el Barbero exista." +
                             "\nVerifique 2) Que la fecha de inicio sea de hoy en adelante." +
                             "\nVerifique 3) Que la fecha de fin sea igual o posterior a la de inicio." +
                             "\nVerifique 4) Que, si es el mismo día, la hora de fin sea posterior a la de inicio.");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateScheduleException(int id, [FromBody] UpdateScheduleExceptionRequest request)
        {
            if (id <= 0) return BadRequest("Id inválido");

            if (_scheduleExceptionService.UpdateScheduleException(id, request))
            {
                return NoContent();
            }

            return BadRequest($"No se pudo actualizar la excepción con ID {id}. " +
                             "\n\nVerifique 1) Que la excepción exista y el Barbero sea válido." +
                             "\nVerifique 2) Que la fecha de inicio sea de hoy en adelante." +
                             "\nVerifique 3) Que la fecha de fin sea igual o posterior a la de inicio." +
                             "\nVerifique 4) Que los rangos de horario sean lógicos (Inicio < Fin).");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteScheduleException(int id)
        {
            if (id <= 0) return BadRequest("Id inválido");

            if (_scheduleExceptionService.DeleteScheduleException(id))
            {
                return NoContent();
            }

            return NotFound($"No se encontró la excepción con ID {id} para eliminar.");
        }
    }
}