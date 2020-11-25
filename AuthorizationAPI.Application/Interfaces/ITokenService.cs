using System.Threading.Tasks;
using AuthorizationAPI.Application.Models;

namespace AuthorizationAPI.Application.Interfaces
{
    public interface ITokenService
    {
        Task<string> GetTokenForUser(UserDto userDto);
    }
}