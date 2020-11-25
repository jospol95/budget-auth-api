using System.Reflection;
using AuthorizationAPI.Application.Commands;
using AuthorizationAPI.Application.Queries;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthorizationAPI.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            // services.AddMediatR(typeof(Assembly));
            // services.AddMediatR(typeof(Assembly));

            // services.AddMediatR(typeof(LoginUserCommandHandler).Assembly, 
            //         // typeof(GetUserDtoQueryHandler).Assembly, 
            //          typeof(RegisterUserCommand.RegisterUserCommandHandler).Assembly);
            
            services.AddMediatR(Assembly.GetExecutingAssembly());
            
            return services;
        }
    }
}