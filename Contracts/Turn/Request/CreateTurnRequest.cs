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
        public DateTime DateTimeTurn { get; set; } 

        [Required]
        public ServiceType Service { get; set; }

        [Required]
        public DateTimeOffset Start { get; set; } 

        [Required]
        public DateTimeOffset End { get; set; } 
    }
}
