namespace Application.Service
{
    public interface IBarberScheduleService
    {
        List<Contracts.BarberSchedule.Response.BarberScheduleResponse> GetAllBarberSchedules();
        bool CreateBarberSchedule(Contracts.BarberSchedule.Request.CreateBarberScheduleRequest request);
        bool UpdateBarberSchedule(int id, Contracts.BarberSchedule.Request.UpdateBarberScheduleRequest request);
        bool DeleteBarberSchedule(int id);
    }
}