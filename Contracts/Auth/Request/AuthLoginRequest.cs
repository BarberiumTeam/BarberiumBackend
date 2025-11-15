using System.ComponentModel.DataAnnotations;

namespace Contracts.Auth.Request
{
    public class AuthLoginRequest
    {
        [Required(ErrorMessage = "El email es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del email no es válido.")]
        public string Email { get; set; } = string.Empty;


        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [DataType(DataType.Password)] 
        [StringLength(100, MinimumLength = 8,
        ErrorMessage = "La contraseña debe tener entre {2} y {1} caracteres.")]
        public string Password { get; set; } = string.Empty;
    }
}