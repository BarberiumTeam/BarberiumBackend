using Application.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    // Define la ruta base para este controlador
    [Route("api/[controller]")]
    [ApiController]
    public class InfoController : ControllerBase
    {
        private readonly IInfoService _infoService;

        // Inyección de tu servicio de aplicación
        public InfoController(IInfoService infoService)
        {
            _infoService = infoService;
        }

        [HttpGet("dolar/blue/venta")]
        [AllowAnonymous] // Permitimos acceso público (sin JWT)
        public async Task<IActionResult> GetBlueDolarPrice(CancellationToken cancellationToken)
        {
            // Llama a la lógica de negocio, que a su vez ejecuta el cliente resiliente (con Polly)
            var dolarRate = await _infoService.GetBlueDolarPriceAsync(cancellationToken);

            if (dolarRate == null)
            {
                // Devolver error si no se encuentra o la API externa falla después de los reintentos
                return NotFound(new { message = "Cotización del Dólar Blue no disponible o servicio externo inaccesible." });
            }

            // Devuelve el resultado al cliente
            return Ok(new
            {
                dolarRate.Nombre,
                PrecioVenta = dolarRate.Venta
            });
        }
    }
}