using Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace Contracts.Turn.Request
{
    public class UpdateTurnRequest
    {
        [Required(ErrorMessage = "Debe especificar un tipo de servicio.")]
        [EnumDataType(typeof(ServiceType), ErrorMessage = "El nombre de servicio no es valido.")]
        public ServiceType Service { get; set; }

        [Required(ErrorMessage = "Debe especificar una fecha.")]
        public DateOnly TurnDate { get; set; }

        [Required(ErrorMessage = "Debe especificar un horario incial.")]
        public TimeOnly TurnStartTime { get; set; }

        [Required(ErrorMessage = "Debe especificar un horario final.")]
        public TimeOnly TurnEndTime { get; set; }
    }
}
