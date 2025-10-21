
namespace Domain
{
    public class ScheduleException : BaseEntity
    {
        public int BarberId { get; set; }
        public Barber Barber {  get; set; }
        public DateOnly ExceptionDate { get; set; }
        public TimeOnly ExceptionStartTime { get; set; }
        public TimeOnly ExceptionEndTime { get; set; }
        public ExceptionType ExceptionType { get; set; }

    }
    public enum ExceptionType
    {
        Holiday,
        Vacation,
        SickLeave,
        PersonalLeave
    }

}
