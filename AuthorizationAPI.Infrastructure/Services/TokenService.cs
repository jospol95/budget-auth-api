using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AuthorizationAPI.Application.Interfaces;
using AuthorizationAPI.Application.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace AuthorizationAPI.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        
        private readonly SymmetricSecurityKey _key;
        private readonly ApplicationSettings _appSettings;

        public TokenService(IOptions<ApplicationSettings> options)
        {
            _appSettings = options.Value;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.AppKey));
            // _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.AppKey));
        }
        
        public async Task<string> GetTokenForUser(UserDto userDto)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, userDto.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, userDto.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, userDto.LastName),
                new Claim(JwtRegisteredClaimNames.Sid, userDto.Id)
                // new Claim(JwtRegisteredClaimNames.)
            };
            
            var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Issuer = "http://wwww.localhost:6001/",
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = credentials
            };
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }


    }
}