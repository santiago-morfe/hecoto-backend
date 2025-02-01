using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using hecotoBackend.Services;
using hecotoBackend.DTOs;
using System.Security.Claims;

namespace hecotoBackend.Controllers.Users
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UsersServices _usersServices;

        public UsersController(UsersServices usersServices)
        {
            _usersServices = usersServices ?? throw new ArgumentNullException(nameof(usersServices));
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetMyProfile()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized();
            }
            var userId = int.Parse(userIdClaim.Value);

            var user = await _usersServices.GetUser(userId);

            return new UserDto
            {
                Id = user.Id.ToString(),
                Name = user.UserName,
                Email = user.Email
            };
        }

        [HttpPut("me")]
        [Authorize]
        public async Task<ActionResult> UpdateProfile([FromBody] UserDto user)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized();
            }
            var userId = int.Parse(userIdClaim.Value);

            await _usersServices.UpdateUser(userId, user);

            return Ok();
        }

        [HttpDelete("me")]
        [Authorize]
        public async Task<ActionResult> DeleteProfile()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized();
            }
            var userId = int.Parse(userIdClaim.Value);

            await _usersServices.DeleteUser(userId);

            return Ok();
        }
        [HttpGet("userId")]
        [Authorize]
        public async Task<ActionResult<ProfileDto>> GetUser(string userId)
        {
            var user = await _usersServices.GetUser(int.Parse(userId));

            return new ProfileDto
            {
                Id = user.Id.ToString(),
                Name = user.UserName,
            };
        }

        [HttpGet("search")]
        [Authorize]
        public async Task<ActionResult<ProfileDto>> SearchUser([FromQuery] string name)
        {
            var user = await _usersServices.GetUserByName(name);
            
            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpGet("{id}/medals")]
        public async Task<IActionResult> GetUserMedals(int id)
        {
            var medals = await _usersServices.GetUserMedals(id);
            return Ok(medals);
        }
    }
}

