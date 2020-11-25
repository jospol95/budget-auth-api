using System.Threading;
using System.Threading.Tasks;
using AuthorizationAPI.Application.Commands.Base;
using AuthorizationAPI.Application.Interfaces;
using AuthorizationAPI.Application.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AuthorizationAPI.Application.Queries
{
    public class GetUserDtoQuery : IRequest<UserDto>
    {
        public string Id {get; set; }

        public GetUserDtoQuery(string id)
        {
            Id = id;
        }
    }

    public class GetUserDtoQueryHandler : UserRequestBase, IRequestHandler<GetUserDtoQuery, UserDto>
    {
        
        public async Task<UserDto> Handle(GetUserDtoQuery request, CancellationToken cancellationToken)
        {
            var user = await UserService.GetUserByIdAsync(request.Id);
            if (user == null) return null;
            
            var userDto = new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                Email = user.Email,
                LastName = user.LastName,
                AuthenticationMethodId = user.GetAuthenticationId()
            };
    
            return userDto;
        }

        public GetUserDtoQueryHandler(IUserService userService, ILoggerFactory loggerFactory, ISecurityService securityService) : base(userService, loggerFactory, securityService)
        {
        }
    }
}