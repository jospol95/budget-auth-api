using System;
using System.Text;
using AuthorizationAPI.Application.Interfaces;
using AuthorizationAPI.Infrastructure.Persistence;
using AuthorizationAPI.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AuthorizationAPI.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();
            var appSettings = configuration.GetSection("ApplicationSettings").Get<ApplicationSettings>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudiences = jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.AppKey)),
                        ClockSkew = TimeSpan.Zero
                    };

                });
            
            return services;
        }
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<AuthDbContext>(opt =>
            {
                // opt.UseNpgsql(configuration.GetConnectionString("AuthDB"));
                opt.UseSqlServer(configuration.GetConnectionString("AuthDBSql"),
                    sqlServerOptionsAction: sqlOptions =>
                    { 
                        //Configuring Connection Resiliency:
                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 5,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null);
                    });
            });

            // Fill application settings for DI from appSettings.json
            services.Configure<ApplicationSettings>(configuration.GetSection(
               "ApplicationSettings" ));

            
            // https://stackoverflow.com/questions/38138100/addtransient-addscoped-and-addsingleton-services-differences
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ISecurityService, SecurityService>();
            services.AddTransient<ITokenService, TokenService>();
            
            
            return services;
        }
    }
}