using Domain.Entity;

namespace Application.Abstraction
{
    public interface IAuthRepository
    {
        // Devuelve 'object' porque puede ser Client o Barber
        object? GetUserByEmail(string email);
        bool EmailExists(string email);
        bool AddClient(Client client);
        bool AddBarber(Barber barber);
    }
}
