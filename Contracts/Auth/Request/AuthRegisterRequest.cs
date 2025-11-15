using System.ComponentModel.DataAnnotations;

namespace Contracts.Auth.Request
{
    public class AuthRegisterRequest
    {
        // Name (Solo Required y Display, si lo necesitas en la UI)
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Name { get; set; } = string.Empty;

        // Email
        [Required(ErrorMessage = "El email es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del email no es válido.")]
        public string Email { get; set; } = string.Empty;

        // Phone (Con validación de formato)
        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [Phone(ErrorMessage = "El número de teléfono no es válido.")]
        public string Phone { get; set; } = string.Empty;

        // Password (Con requisitos de longitud y tipo de dato)
        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [DataType(DataType.Password)] // Para ocultar la entrada en la UI

        // La contraseña debe tener entre 8 y 100 caracteres.
        [StringLength(100, MinimumLength = 8,
            ErrorMessage = "La contraseña debe tener entre {2} y {1} caracteres.")]
        public string Password { get; set; } = string.Empty;
    }
}