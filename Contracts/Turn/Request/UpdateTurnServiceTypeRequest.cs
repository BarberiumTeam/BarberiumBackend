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
        [Required]
        [EnumDataType(typeof(ServiceType))]
        public ServiceType NewServiceType { get; set; }
    }
}
