using Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace Contracts.BarberSchedule.Request
{
    public class UpdateBarberScheduleRequest
    {
        [Required(ErrorMessage = "El ID del barbero es obligatorio.")]
        public int BarberId { get; set; }

        [Required(ErrorMessage = "La hora de inicio es obligatoria.")]
        [DataType(DataType.Time)]
        public TimeOnly StartTime { get; set; }

        [Required(ErrorMessage = "La hora de fin es obligatoria.")]
        [DataType(DataType.Time)]
        public TimeOnly EndTime { get; set; }

        [Required(ErrorMessage = "El día de la semana es obligatorio.")]
        [EnumDataType(typeof(WeekDay))] // se asegura que el valor sea válido para el enum
        public WeekDay WeekDay { get; set; }
    }
}
