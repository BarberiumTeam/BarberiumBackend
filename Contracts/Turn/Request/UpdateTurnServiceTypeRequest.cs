using Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Turn.Request
{
    public class UpdateTurnServiceTypeRequest
    {
        [Required(ErrorMessage = "Debe especificar un tipo de servicio.")]
        [EnumDataType(typeof(ServiceType), ErrorMessage = "Ingrese un nuevo servicio valido")]
        public ServiceType NewServiceType { get; set; }
    }
}
