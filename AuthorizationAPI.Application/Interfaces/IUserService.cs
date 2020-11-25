using System.Threading.Tasks;
using AuthorizationAPI.Domain.Models;

namespace AuthorizationAPI.Application.Interfaces
{
    public interface IUserService
    { 
        Task<User> GetUserByIdAsync(string id);
        Task<User> GetUserByEmailAsync(string email);

        Task<string> SaveUserAsync(string email, byte[] passwordHash, byte[] passwordSalt, string firstName, string lastName,
            int authenticationTypeId);


    }
}