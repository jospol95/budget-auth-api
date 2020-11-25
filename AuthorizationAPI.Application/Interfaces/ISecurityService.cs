using System.Threading.Tasks;

namespace AuthorizationAPI.Application.Interfaces
{
    public interface ISecurityService
    {
        void GetPasswordHashAndSalt(string password, out byte[] passwordHash, out byte[] passwordSalt);

        Task<bool> ValidatePasswordHashAndSaltAsync(string password, byte[] userPasswordHash, byte[] userPasswordSalt);

    }
}