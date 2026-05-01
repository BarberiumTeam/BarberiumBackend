using Application.Service;
using Contracts.Barber.Request;
using Contracts.Barber.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BarberController : ControllerBase
    {
        private readonly IBarberService _barberService;

        public BarberController(IBarberService barberService)
        {
            _barberService = barberService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BarberResponse>> GetAllBarbers()
        {
            var barbers = _barberService.GetAllBarbers();
            return Ok(barbers);
        }

        [HttpGet("{barberId}")]
        public ActionResult<BarberResponse?> GetBarberById(int barberId)
        {
            var barber = _barberService.GetBarberById(barberId);

            if (barber == null)
            {
                return NotFound();
            }

            return Ok(barber);
        }


        [HttpPut("{id}")]
        public IActionResult UpdateClient(int id, [FromBody] UpdateBarberRequest request)
        {
            // 🔴 Validación básica
            if (id <= 0)
                return BadRequest("Id invalido");

            if (request == null)
                return BadRequest("Datos inválidos");

            // 💾 Intentar actualizar (el service ya valida todo)
            bool success = _barberService.UpdateBarber(id, request);

            if (!success)
            {
                // 🔎 Verificar si existe para diferenciar error
                var barber = _barberService.GetBarberById(id);

                if (barber == null)
                    return NotFound($"Barber con {id} no encontrado");

                return BadRequest("El email ya está registrado.");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBarber(int id)
        {
            if (id <= 0) return BadRequest("Id invalido");

            bool success = _barberService.DeleteBarber(id);

            if (!success)
            {
                return NotFound($"Barber con el {id} no fue encontrado y no se pudo borrar");
            }

            return NoContent();
        }

        [HttpGet("has-any")]
        public ActionResult<bool> HasAnyBarber()
        {
            var exists = _barberService.HasAnyBarber();
            return Ok(exists);
        }
    }
}
