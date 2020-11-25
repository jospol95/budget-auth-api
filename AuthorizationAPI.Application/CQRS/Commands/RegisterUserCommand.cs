using AuthorizationAPI.Application.Models.Enums;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using AuthorizationAPI.Application.Commands.Base;
using AuthorizationAPI.Application.Exceptions;
using AuthorizationAPI.Application.Interfaces;
using Microsoft.Extensions.Logging;


namespace AuthorizationAPI.Application.Commands
{
    public class RegisterUserCommand : IRequest<string>
    {
        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        [StringLength(24)]
        public string Password { get; set; }
        
        [Required]
        [StringLength(60)]
        public string FirstName { get; set; }
        
        [Required]
        [StringLength(60)]
        public string LastName { get; set; }
        
        public AuthenticationTypeEnum? AuthenticationType { get; set; }


        public class RegisterUserCommandHandler : UserRequestBase, IRequestHandler<RegisterUserCommand, string>
        {
            public async Task<string> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
            {
                var isEmailTaken = await UserService.GetUserByEmailAsync(request.Email) != null;
                if(isEmailTaken) throw new EmailAlreadyRegisteredException();

                request.AuthenticationType ??= AuthenticationTypeEnum.AuthorizationApi;
                SecurityService.GetPasswordHashAndSalt(request.Password, out var passwordHash, out var passwordSalt);
                
                var addedUserId = await UserService.SaveUserAsync(request.Email, passwordHash, passwordSalt, request.FirstName, request.LastName,
                    (int)request.AuthenticationType);

                // return Unit.Value;
                return addedUserId;
            }


            public RegisterUserCommandHandler(IUserService userService, ILoggerFactory loggerFactory, ISecurityService securityService) : base(userService, loggerFactory, securityService)
            {
            }
        }

        
    }
}