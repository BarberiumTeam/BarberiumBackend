using Domain.Entity;

namespace Application.Abstraction
{
    public interface IScheduleExceptionRepository
    {
        ScheduleException? GetScheduleExceptionById(int id);
        List<ScheduleException> GetAllScheduleExceptions();
        bool CreateScheduleException(ScheduleException scheduleException);
        bool UpdateScheduleException(ScheduleException scheduleException);
        bool DeleteScheduleException(int id);
    }
}
