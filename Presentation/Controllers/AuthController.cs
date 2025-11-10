using Application.Service;
using Contracts.Auth.Request;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register/client")]
        public async Task<IActionResult> RegisterClient([FromBody] AuthRegisterRequest request)
        {
            var response = await _authService.RegisterClient(request);
            return response == null ? BadRequest("El email ya está registrado.") : Ok(response);
        }
        [HttpPost("register/barber")]
        public async Task<IActionResult> RegisterBarber([FromBody] AuthRegisterRequest request)
        {
            var response = await _authService.RegisterBarber(request);
            return response == null ? BadRequest("El email ya esta registrado. ") : Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthLoginRequest request)
        {
            var response = await _authService.Login(request);
            return response == null ? Unauthorized("Email o contraseña inválidos.") : Ok(response);
        }
    }
}
