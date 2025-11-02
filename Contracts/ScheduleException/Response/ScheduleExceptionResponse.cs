using Domain.Entity;

namespace Contracts.ScheduleException.Response
{
    public class ScheduleExceptionResponse
    {
        public int Id { get; set; }
        public int BarberId { get; set; }
        public string BarberName { get; set; } = string.Empty;
        public DateOnly ExceptionDate { get; set; }
        public TimeOnly ExceptionStartTime { get; set; }
        public TimeOnly ExceptionEndTime { get; set; }
        public ExceptionType ExceptionType { get; set; }
    }
}
