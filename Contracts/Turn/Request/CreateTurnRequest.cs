using Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace Contracts.Turn.Request
{
    public class CreateTurnRequest
    {

        [Required(ErrorMessage = "El ID del cliente es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del cliente debe ser un número positivo (mínimo 1).")]
        public int ClientId { get; set; }

        [Required(ErrorMessage = "El ID del barbero es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del barbero debe ser un número positivo (mínimo 1).")]
        public int BarberId { get; set; }

        [Required(ErrorMessage = "Debe especificar un tipo de servicio.")]
        [EnumDataType(typeof(ServiceType), ErrorMessage = "El nombre de servicio no es valido.")]
        public ServiceType? Service { get; set; }

        [Required(ErrorMessage = "Debe especificar una fecha.")]
        public DateOnly? TurnDate { get; set; }

        [Required(ErrorMessage = "Debe especificar un horario incial.")]
        public TimeOnly? TurnStartTime { get; set; }

        [Required(ErrorMessage = "Debe especificar un horario final.")]
        public TimeOnly? TurnEndTime { get; set; }
    }
}
