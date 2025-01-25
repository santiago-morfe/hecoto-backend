// modelos para la autenticasion y autorizacion 

using System.ComponentModel.DataAnnotations;

namespace hecotoBackend.DTOs
{
    public class LoginDto
    {
        [Required]
        public required string Name { get; set; }
        
        [Required]
        public required string Password { get; set; }
    }

    public class RegisterDto
    {
        [Required]
        public required  string Name { get; set; }

        [Required]
        public required  string Password { get; set; }

        [Required]
        [EmailAddress]
        public required  string Email { get; set; }
    }

    public class RefreshTokenDto
    {
        [Required]
        public required  string RefreshToken { get; set; }
    }

    public class TokenDto
    {
        public required  string Token { get; set; }
        public required  string RefreshToken { get; set; }
    }

    public class UserDto
    {
        public required  string Id { get; set; }
        public required  string Name { get; set; }
        public required  string Email { get; set; }
    }

    public class UserWithTokenDto
    {
        public required  string Id { get; set; }
        public required  string Name { get; set; }
        public required  string Email { get; set; }
        public required  string Token { get; set; }
        public required  string RefreshToken { get; set; }
    }
}