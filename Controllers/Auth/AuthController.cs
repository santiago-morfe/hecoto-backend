// controlador para la autenticación de usuarios en la API 
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using hecotoBackend.Data;
using hecotoBackend.DTOs;
using hecotoBackend.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;


namespace hecotoBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly AuthServices _authServices;


        public AuthController(AppDbContext context, AuthServices authServices)
        {
            _context = context;
            _authServices = authServices;

        }

        // POST: api/Auth/Register

        [HttpPost("register")]
        public async Task<ActionResult<UserWithTokenDto>> Register([FromBody] RegisterDto register)
        {
            var user = await _authServices.Register(register);
            if (user == null)
            {
                return BadRequest();
            }
            else
            {
                // devolver el usuario creado con un estatus 201 y los tokens
                var token = _authServices.GenerateAccessToken(user);
                var refreshToken = await _authServices.GenerateRefreshToken(user);
                // responder con la informacino del usuario y los tokens
                return CreatedAtAction(nameof(Register), new UserWithTokenDto
                {
                    Id = user.Id.ToString(),
                    Name = user.UserName,
                    Email = user.Email,
                    Token = token,
                    RefreshToken = refreshToken
                });
            }
        }

        // POST: api/Auth/Login
        [HttpPost("login")]
        public async Task<ActionResult<UserWithTokenDto>> Login([FromBody] LoginDto login)
        {
            var user = await _authServices.ValidateCredentials(login);
            if (user == null)
            {
                return Unauthorized();
            }
            else
            {
                // devolver el usuario creado con un estatus 201 y los tokens
                var token = _authServices.GenerateAccessToken(user);
                var refreshToken = await _authServices.GenerateRefreshToken(user);
                // responder con la informacino del usuario y los tokens
                return CreatedAtAction(nameof(Login), new UserWithTokenDto
                {
                    Id = user.Id.ToString(),
                    Name = user.UserName,
                    Email = user.Email,
                    Token = token,
                    RefreshToken = refreshToken
                });
            }
        }

        // POST: api/Auth/RefreshToken
        [HttpPost("refreshToken")]
        public async Task<ActionResult<UserWithTokenDto>> RefreshToken([FromBody] RefreshTokenDto refreshToken)
        {
            var user = await _authServices.ValidateRefreshToken(refreshToken.RefreshToken);
            if (user == null)
            {
                return Unauthorized();
            }
            else
            {
                // devolver el usuario creado con un estatus 201 y los tokens
                var token = _authServices.GenerateAccessToken(user);
                var newRefreshToken = await _authServices.GenerateRefreshToken(user);
                // responder con la informacino del usuario y los tokens
                return CreatedAtAction(nameof(RefreshToken), new UserWithTokenDto
                {
                    Id = user.Id.ToString(),
                    Name = user.UserName,
                    Email = user.Email,
                    Token = token,
                    RefreshToken = newRefreshToken
                });
            }
        }

        // GET: api/Auth/logout
        [Authorize]
        [HttpGet("logout")]
        public async Task<ActionResult> Logout()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var user = await _context.Users.FindAsync(int.Parse(userId));
            if (user == null)
            {
                return Unauthorized();
            }

            var tokens = await _context.JwtTokens.Where(t => t.UserId == user.Id).ToListAsync();
            _context.JwtTokens.RemoveRange(tokens);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}