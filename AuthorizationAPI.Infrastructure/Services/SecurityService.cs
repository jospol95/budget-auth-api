using System.Linq;
using System.Threading.Tasks;
using AuthorizationAPI.Application.Interfaces;

namespace AuthorizationAPI.Infrastructure.Services
{
    public class SecurityService : ISecurityService
    {
        public void GetPasswordHashAndSalt(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        public async Task<bool> ValidatePasswordHashAndSaltAsync(string password, byte[] userPasswordHash, byte[] userPasswordSalt)
        {
            var hmac = new System.Security.Cryptography.HMACSHA512(userPasswordSalt);
            var computerHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            if(computerHash.Where((value, count) => value != userPasswordHash[count]).Any())
            {
                return false;
            }

            return true;
        }

    }
}