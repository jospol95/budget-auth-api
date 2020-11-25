using System;
using System.Linq;
using System.Threading.Tasks;
using AuthorizationAPI.Application.Interfaces;
using AuthorizationAPI.Domain.Models;
using AuthorizationAPI.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AuthorizationAPI.Infrastructure.Services
{
     public class UserService : IUserService
    {
        private readonly AuthDbContext _authDbContext;

        public UserService(AuthDbContext authDbContext)
        {
            _authDbContext = authDbContext;
        }
        
        public async Task<User> GetUserByIdAsync(string id)
        {
            var existingUser = await _authDbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            return existingUser;
        }
        public async Task<User> GetUserByEmailAsync(string email)
        {
            var existingUser = await _authDbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            return existingUser;
        }
        
        public async Task<string> SaveUserAsync(string email, byte[] passwordHash, byte[] passwordSalt, string firstName,string lastName, 
             int authenticationTypeId)
        {
            // GetPasswordHashAndSalt(password, out var passwordHash, out var passwordSalt);
            var user = new User( Guid.NewGuid().ToString(), email, passwordHash, passwordSalt, firstName, lastName, authenticationTypeId);

            await _authDbContext.Users.AddAsync(user);
            await _authDbContext.SaveChangesAsync();

            return user.Id;
        }

    }
}