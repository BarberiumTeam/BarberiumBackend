
using Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace Contracts.ScheduleException.Request
{
    public class UpdateScheduleExceptionRequest
    {
        // Solo permitimos actualizar los detalles de la excepción, no el Barbero.
        [Required]
        [DataType(DataType.Date)] 
        public DateOnly ExceptionStartDate { get; set; }

        [Required]
        [DataType(DataType.Date)] 
        public DateOnly ExceptionEndDate { get; set; }

        [Required]
        public TimeOnly ExceptionStartTime { get; set; }

        [Required]
        public TimeOnly ExceptionEndTime { get; set; }

        [Required]
        [EnumDataType(typeof(ExceptionType))]
        public ExceptionType ExceptionType { get; set; }
    }
}
