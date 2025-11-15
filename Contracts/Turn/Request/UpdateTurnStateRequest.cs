using Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Turn.Request
{
    public class UpdateTurnStateRequest
    {
        [Required(ErrorMessage = "Ingresa un estado")]
        [EnumDataType(typeof(State), ErrorMessage = "Ingrese un nuevo estado valido")]
        public State NewState { get; set; }
    }
}
