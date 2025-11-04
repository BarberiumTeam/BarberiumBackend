using Domain.Entity;

namespace Contracts.ScheduleException.Response
{
    public class ScheduleExceptionResponse
    {
        public int Id { get; set; }
        public int BarberId { get; set; }
        public string Barber { get; set; } = string.Empty;
        public DateOnly ExceptionStartDate { get; set; }
        public DateOnly ExceptionEndDate { get; set; }
        public TimeOnly ExceptionStartTime { get; set; }
        public TimeOnly ExceptionEndTime { get; set; }
        public ExceptionType ExceptionType { get; set; }
    }
}
