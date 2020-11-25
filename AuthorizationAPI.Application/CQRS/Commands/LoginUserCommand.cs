using System.Threading;
using System.Threading.Tasks;
using AuthorizationAPI.Application.Commands.Base;
using AuthorizationAPI.Application.Exceptions;
using AuthorizationAPI.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AuthorizationAPI.Application.Commands
{
    public class LoginUserCommand : IRequest<string>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginUserCommandHandler : UserRequestBase, IRequestHandler<LoginUserCommand, string>
    {


        public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await UserService.GetUserByEmailAsync(request.Email);
            // if(existingUser == null) throw new EmailAndOrPasswordIncorrectException();
            if(existingUser == null) throw new EmailNotRegisteredException();
            
            
            existingUser.GetPasswordHashAndSalt(out var passwordHash, out var passwordSalt);
            
            var passwordMatched = await SecurityService.ValidatePasswordHashAndSaltAsync(request.Password,
                passwordHash, passwordSalt);
            if (!passwordMatched) throw new EmailAndOrPasswordIncorrectException();

            // return Unit.Value;

            return existingUser.Id;
        }


        public LoginUserCommandHandler(IUserService userService, ILoggerFactory loggerFactory, ISecurityService securityService) : base(userService, loggerFactory, securityService)
        {
        }
    }


}