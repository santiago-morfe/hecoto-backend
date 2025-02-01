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
    public class UsersServices
    {
        private readonly AppDbContext _context;

        public UsersServices(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<UsersModel> GetUser(int userId)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == userId) ?? throw new InvalidOperationException("User not found.");
            return user;
        }

        public async Task<UsersModel> UpdateUser(int userId, UserDto user)
        {
            var userToUpdate = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == userId) ?? throw new InvalidOperationException("User not found.");

            userToUpdate.UserName = user.Name;
            userToUpdate.Email = user.Email;

            _context.Users.Update(userToUpdate);
            await _context.SaveChangesAsync();

            return userToUpdate;
        }

        public async Task<ProfileDto> GetUserByName(string userName)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.UserName == userName) ?? throw new InvalidOperationException("User not found.");
            return new ProfileDto
            {
                Id = user.Id.ToString(),
                Name = user.UserName
            };
        }
        //DeleteUser
        public async Task DeleteUser(int userId)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == userId) ?? throw new InvalidOperationException("User not found.");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
        // GetUserMedals
        public async Task<IEnumerable<MedalsModel>> GetUserMedals(int userId)
        {
            var user = await _context.Users
                .Include(u => u.Medals)
                .FirstOrDefaultAsync(u => u.Id == userId) ?? throw new InvalidOperationException("User not found.");
            
            var medals = user.Medals ?? throw new InvalidOperationException("User has no medals.");
            return user.Medals;
        }
    }
}
