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
            bool success = _scheduleExceptionService.CreateScheduleException(request);
            if (!success)
            {
                return BadRequest("No se creo la excepcion de horario, revise nuevamente los datos");
            }
            return Created();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateScheduleException(int id, [FromBody] UpdateScheduleExceptionRequest request)
        {
            if (id <= 0) return BadRequest("Id invalido");
            bool success = _scheduleExceptionService.UpdateScheduleException(id, request);
            if (!success)
            {
                return BadRequest("No se actualizo la excepcion de horario, revise nuevamente los datos");
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteScheduleException(int id)
        {
            if (id <= 0) return BadRequest("Id invalido");
            bool success = _scheduleExceptionService.DeleteScheduleException(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}