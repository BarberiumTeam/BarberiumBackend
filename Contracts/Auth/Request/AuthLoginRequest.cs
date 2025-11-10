using System.ComponentModel.DataAnnotations;

namespace Contracts.Auth.Request
{
    public class AuthLoginRequest
    {
        [Required] public string Email { get; set; } = string.Empty;
        [Required] public string Password { get; set; } = string.Empty;
    }
}