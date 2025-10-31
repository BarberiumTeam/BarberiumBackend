using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Barber.Request
{
    public class CreateBarberRequest
    {
        [Required(ErrorMessage = "El campo es obligatorio")] // fijarse como arreglarlo
        public string Name { get; set; }
        public string Specialty { get; set; }
    }
}
