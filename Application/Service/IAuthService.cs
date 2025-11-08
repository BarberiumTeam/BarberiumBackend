using Contracts.Auth.Request;
using Contracts.Auth.Response;

namespace Application.Service
{
    public interface IAuthService
    {
        // Usamos Task para simular operaciones asíncronas de repositorio
        Task<AuthResponse?> Login(LoginRequest request);
        Task<AuthResponse?> RegisterClient(RegisterRequest request);
        Task<AuthResponse?> RegisterBarber(RegisterRequest request);
    }
}
