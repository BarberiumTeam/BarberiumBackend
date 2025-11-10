using BCrypt.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Security
{
    public static class PasswordHasher
    {
        //Toma la contraseña y usa el algoritmo BCrypt
        //para generar una cadena larga, aleatoria y única
        public static string Hash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        //Toma la contraseña que el usuario ingresa durante el Login
        //Luego la compara con el hash guardado en la base de datos
        //sin tener que desencriptar nada.
        public static bool Verify(string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }
    }
}
