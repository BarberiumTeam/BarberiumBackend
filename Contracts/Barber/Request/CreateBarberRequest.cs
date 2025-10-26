using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Barber.Request
{
    public class CreateBarberRequest
    {
        public string Name { get; set; }
        public string Specialty { get; set; }
    }
}
