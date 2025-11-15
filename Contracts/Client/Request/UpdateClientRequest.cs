using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Client.Request
{
    public class UpdateClientRequest
    {
        // No voy a incluir el ID por que lo voy a pasar por el DTO.
        // Porque lo vamos a pedir en el URL.
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;


    }
}
