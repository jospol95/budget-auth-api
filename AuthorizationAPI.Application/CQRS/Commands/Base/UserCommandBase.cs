using System;
using AuthorizationAPI.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace AuthorizationAPI.Application.Commands.Base
{
    public class UserRequestBase
    {
        protected readonly IUserService UserService;
        protected readonly ISecurityService SecurityService;
        protected readonly ILogger _logger;

        protected UserRequestBase(IUserService userService,
            ILoggerFactory loggerFactory,
            ISecurityService securityService)
        {
            UserService = userService;
            SecurityService = securityService;
            _logger = loggerFactory.CreateLogger("UserAuthorization");
        }

        protected Exception LogErrorAndThrowException(string exceptionMessage, Exception exception = null)
        {
            // TODO User LogLevel
            _logger.LogError(exceptionMessage);
            throw new Exception(exceptionMessage);
        }
    }
}