using Application.Abstraction;
using Application.Security;
using Contracts.Auth.Request;
using Contracts.Auth.Response;
using Domain.Entity;

namespace Application.Service
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly ITokenService _tokenService;

        public AuthService(IAuthRepository authRepository, ITokenService tokenService)
        {
            _authRepository = authRepository;
            _tokenService = tokenService;
        }

        public Task<AuthResponse?> RegisterClient(AuthRegisterRequest request)
        {
            if (_authRepository.EmailExists(request.Email)) return Task.FromResult<AuthResponse?>(null);

            var client = new Client
            {
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                PasswordHash = PasswordHasher.Hash(request.Password), //  HASHING
                Role = "Client"
            };

            if (_authRepository.AddClient(client))
            {
                var response = new AuthResponse
                {
                    Token = _tokenService.GenerateToken(client, client.Role),
                    Role = client.Role,
                    UserId = client.Id
                };
                return Task.FromResult<AuthResponse?>(response);
            }
            return Task.FromResult<AuthResponse?>(null);
        }
        public Task<AuthResponse>? RegisterBarber(AuthRegisterRequest request)
        {
            if (_authRepository.EmailExists(request.Email)) return Task.FromResult<AuthResponse?>(null);
            var barber = new Barber
            {
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                PasswordHash = PasswordHasher.Hash(request.Password), //  HASHING
                Role = "Barber"
            };
            if (_authRepository.AddBarber(barber))
            {
                var response = new AuthResponse
                {
                    Token = _tokenService.GenerateToken(barber, barber.Role),
                    Role = barber.Role,
                    UserId = barber.Id
                };
                return Task.FromResult<AuthResponse?>(response);
            }
            return Task.FromResult<AuthResponse?>(null);
        }


        public Task<AuthResponse?> Login(AuthLoginRequest request)
        {
            var user = _authRepository.GetUserByEmail(request.Email);
            if (user == null) return Task.FromResult<AuthResponse?>(null);

            // Uso de Reflection para obtener propiedades (ya que 'user' es 'object')
            string passwordHash = (string)user.GetType().GetProperty("PasswordHash")!.GetValue(user)!;
            string role = (string)user.GetType().GetProperty("Role")!.GetValue(user)!;
            int userId = (int)user.GetType().GetProperty("Id")!.GetValue(user)!;

            if (PasswordHasher.Verify(request.Password, passwordHash)) //  VERIFICACIÓN
            {
                var response = new AuthResponse
                {
                    Token = _tokenService.GenerateToken(user, role),
                    Role = role,
                    UserId = userId
                };
                return Task.FromResult<AuthResponse?>(response);
            }
            return Task.FromResult<AuthResponse?>(null);
        }
    }
}
