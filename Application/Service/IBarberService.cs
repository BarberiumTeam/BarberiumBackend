using Contracts.Barber.Request;
using Contracts.Barber.Response;

namespace Application.Service
{
    public interface IBarberService
    {
        List<BarberResponse> GetAllBarbers();
        BarberResponse? GetBarberById(int barberId);

        bool CreateBarber(CreateBarberRequest request);
        bool UpdateBarber(int id, UpdateBarberRequest request);
        bool DeleteBarber(int id);
    }
}