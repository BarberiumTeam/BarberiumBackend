
using Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace Contracts.ScheduleException.Request
{
    public class UpdateScheduleExceptionRequest
    {
        // Solo permitimos actualizar los detalles de la excepción, no el Barbero.
        [Required(ErrorMessage = "Debe especificar una fecha inicial")]
        [DataType(DataType.Date)] // Sugerencia de tipo para UI/API
        public DateOnly? ExceptionStartDate { get; set; }

        [Required(ErrorMessage = "Debe especificar una fecha final")]
        [DataType(DataType.Date)] // Sugerencia de tipo para UI/API
        public DateOnly? ExceptionEndDate { get; set; }

        [Required(ErrorMessage = "Debe especificar un horario incial.")]
        [DataType(DataType.Time)]
        public TimeOnly? ExceptionStartTime { get; set; }

        [Required(ErrorMessage = "Debe especificar un horario final.")]
        [DataType(DataType.Time)]
        public TimeOnly? ExceptionEndTime { get; set; }

        [Required(ErrorMessage = "Debe especificar un tipo de excepción.")]
        [EnumDataType(typeof(ExceptionType), ErrorMessage = "El nombre de excepción no es válido.")]
        public ExceptionType ExceptionType { get; set; }
    }
}
