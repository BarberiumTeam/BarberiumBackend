using Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace Contracts.ScheduleException.Request
{
    public class CreateScheduleExceptionRequest
    {
        [Required]
        public int BarberId { get; set; }

        [Required]
        [DataType(DataType.Date)] // Sugerencia de tipo para UI/API
        public DateOnly ExceptionDate { get; set; }

        [Required]
        public TimeOnly ExceptionStartTime { get; set; }

        [Required]
        public TimeOnly ExceptionEndTime { get; set; }

        [Required]
        [EnumDataType(typeof(ExceptionType))] // Asegura que el enum sea válido
        public ExceptionType ExceptionType { get; set; }
    }
}
