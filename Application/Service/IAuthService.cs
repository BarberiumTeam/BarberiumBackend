using Contracts.Auth.Request;
using Contracts.Auth.Response;

namespace Application.Service
{
    public interface IAuthService
    {
        // Usamos Task para simular operaciones asíncronas de repositorio
        Task<AuthResponse?> Login(AuthLoginRequest request);
        Task<AuthResponse?> RegisterClient(AuthRegisterRequest request);
        Task<AuthResponse?> RegisterBarber(AuthRegisterRequest request);
    }
}
