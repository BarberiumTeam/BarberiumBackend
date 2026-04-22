using Application.Service;
using Contracts.BarberSchedule.Request;
using Contracts.BarberSchedule.Response;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IBarberScheduleService _barberScheduleService;

        public ScheduleController(IBarberScheduleService barberScheduleService)
        {
            _barberScheduleService = barberScheduleService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BarberScheduleResponse>> GetAllBarberSchedules()
        {
            var schedule = _barberScheduleService.GetAllBarberSchedules();
            return Ok(schedule);
        }

        [HttpGet("{id}")]
        public ActionResult<BarberScheduleResponse?> GetBarberScheduleById(int id)
        {
            var schedule = _barberScheduleService.GetBarberScheduleById(id);

            if (schedule == null)
            {
                return NotFound();
            }

            return Ok(schedule);
        }

        [HttpPost]
        public IActionResult CreateBarberSchedule([FromBody] CreateBarberScheduleRequest request)
        {
            if (_barberScheduleService.CreateBarberSchedule(request))
            {
                return Created();
            }

            return BadRequest("No se pudo crear el cronograma. " +
                              "\n\nVerifique 1) Que el Barbero exista." +
                              "\nVerifique 2) Que la hora de fin sea posterior a la hora de inicio.");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBarberSchedule(int id, [FromBody] UpdateBarberScheduleRequest request)
        {
            if (id <= 0) return BadRequest("Id inválido");

            if (_barberScheduleService.UpdateBarberSchedule(id, request))
            {
                return NoContent();
            }

            return BadRequest($"No se pudo actualizar el cronograma con ID {id}. " +
                              "\n\nVerifique 1) Que el cronograma exista y el Barbero sea válido." +
                              "\nVerifique 2) Que la hora de fin sea posterior a la hora de inicio.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBarberSchedule(int id)
        {
            if (id <= 0) return BadRequest("Id inválido");

            if (_barberScheduleService.DeleteBarberSchedule(id))
            {
                return NoContent();
            }

            return NotFound($"El cronograma con el ID {id} no se encuentra y por ende no se puede borrar.");
        }
    }
}