using Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace Contracts.Turn.Request
{
    public class CreateTurnRequest
    {
        
        [Required]
        public int ClientId { get; set; }

        [Required]
        public int BarberId { get; set; }

        [Required]
        public ServiceType Service { get; set; }

        [Required]
        public DateOnly TurnDate { get; set; }

        [Required]
        public TimeOnly TurnStartTime { get; set; }

        [Required]
        public TimeOnly TurnEndTime { get; set; }
    }
}
