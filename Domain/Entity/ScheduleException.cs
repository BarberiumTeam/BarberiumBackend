namespace Domain.Entity
{
    public class ScheduleException : BaseEntity
    {
        public int BarberId { get; set; }
        public Barber? Barber {  get; set; }
        public DateOnly ExceptionStartDate { get; set; }
        public DateOnly ExceptionEndDate { get; set; }
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
