using Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace Contracts.Turn.Request
{
    public class UpdateTurnRequest
    {
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
