using Contracts.ScheduleException.Request;
using Contracts.ScheduleException.Response;

namespace Application.Service
{
    public interface IScheduleExceptionService
    {
        List<ScheduleExceptionResponse> GetAllScheduleExceptions();
        ScheduleExceptionResponse? GetScheduleExceptionById(int id);
        bool CreateScheduleException(CreateScheduleExceptionRequest request);
        bool UpdateScheduleException(int id, UpdateScheduleExceptionRequest request);
        bool DeleteScheduleException(int id);
    }
}