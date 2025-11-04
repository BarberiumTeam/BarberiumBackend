using Application.Service;
using Contracts.BarberSchedule.Request;
using Contracts.BarberSchedule.Response;
using Microsoft.AspNetCore.Http;
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

            bool sucess = _barberScheduleService.CreateBarberSchedule(request);
            {
                if (!sucess)
                {
                    return BadRequest("No se creo el nuevo cronograma, revise nuevamente los datos");
                }

                return Created();
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBarberSchedule(int id, [FromBody] UpdateBarberScheduleRequest request)
        {

            if (id <= 0) return BadRequest("Id invalido");

            bool sucess = _barberScheduleService.UpdateBarberSchedule(id, request);

            if (!sucess)
            {
                return NotFound($"El barbero con {id} no se encuentra o no se pudo actualizar");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBarberSchedule(int id) 
        {
            if (id <= 0) return BadRequest("Id invalido");

            bool success = _barberScheduleService.DeleteBarberSchedule(id);

            if (!success)
            {
                return NotFound($"Barber con el id {id} no se encuentra en el cronograma y por ende no se puede borrar ");
            }

            return NoContent();

        }
        

    }
}
