using hecotoBackend.Data;
using hecotoBackend.Models;
using hecotoBackend.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace hecotoBackend.Services
{
    public class AuthServices
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthServices(AppDbContext context, IConfiguration configuration)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

        // Guardar nuevo usuario en la base de datos
        public async Task<UsersModel> Register(RegisterDto register)
        {
            // Verificar si el usuario ya existe
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.UserName == register.Name);
            
            if (existingUser != null)
            {
                throw new InvalidOperationException("User already exists.");
            }
            var user = new UsersModel
            {
                UserName = register.Name,
                Email = register.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(register.Password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        // Validar credenciales de usuario
        public async Task<UsersModel?> ValidateCredentials(LoginDto login)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.UserName == login.Name);

            if (user == null || !BCrypt.Net.BCrypt.Verify(login.Password, user.PasswordHash))
            {
                return null;
            }

            return user;
        }

        // Generar un nuevo token de acceso
        public string GenerateAccessToken(UsersModel user)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var keyString = jwtSettings["Key"];

            if (string.IsNullOrEmpty(keyString))
            {
                throw new InvalidOperationException("JWT Key is not configured properly in appsettings.json.");
            }

            var key = Encoding.ASCII.GetBytes(keyString);
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                [
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email)
                ]),
                Expires = DateTime.UtcNow.AddHours(_configuration.GetValue<int>("Jwt:AccessTokenExpirationMinutes")),
                NotBefore = DateTime.UtcNow,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        // Generar un nuevo token de refresco y guardarlo en la base de datos
        public async Task<string> GenerateRefreshToken(UsersModel user)
        {
            var token = new RefreshTokensModel
            {
                UserId = user.Id,
                RefreshToken = Guid.NewGuid().ToString(),
                Expiration = DateTime.UtcNow.AddDays(_configuration.GetValue<int>("Jwt:RefreshTokenExpirationDays")),
                IsValid = true,
            };

            _context.RefreshTokens.Add(token);
            await _context.SaveChangesAsync();

            return token.RefreshToken;
        }

        // Validar el token de refresco
        public async Task<UsersModel?> ValidateRefreshToken(string refreshToken)
        {
            var token = await _context.RefreshTokens
                .FirstOrDefaultAsync(t => t.RefreshToken == refreshToken);

            if (token == null || token.Expiration < DateTime.UtcNow || !token.IsValid)
            {
                return null;
            }

            return await _context.Users.FindAsync(token.UserId);
        }

        // Invalidar el token de refresco
        public async Task InvalidateRefreshToken(string refreshToken)
        {
            var token = await _context.RefreshTokens
                .FirstOrDefaultAsync(t => t.RefreshToken == refreshToken);

            if (token != null)
            {
                token.IsValid = false;
                await _context.SaveChangesAsync();
            }
        }
    }
}