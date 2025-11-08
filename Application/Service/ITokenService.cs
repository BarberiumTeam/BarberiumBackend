using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public interface ITokenService
    {
        // Recibe el objeto del usuario (Client/Barber) y su rol
        string GenerateToken(object user, string role);
    }
}
