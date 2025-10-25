using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Client.Request
{
    public class CreateClientRequest
    {
        //No le vamos a pedir el ID porque se genera solo por el EF
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        //Mas adelante vamos a crear la password y hashearla.

    }
}
