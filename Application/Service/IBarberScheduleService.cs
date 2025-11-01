using Contracts.BarberSchedule.Response;
using Contracts.BarberSchedule.Request;

namespace Application.Service
{
    public interface IBarberScheduleService
    {
        List<BarberScheduleResponse> GetAllBarberSchedules();
        BarberScheduleResponse? GetBarberScheduleById(int BarberId);
        bool CreateBarberSchedule(CreateBarberScheduleRequest request);
        bool UpdateBarberSchedule(int id, UpdateBarberScheduleRequest request);
        bool DeleteBarberSchedule(int id);
    }
}