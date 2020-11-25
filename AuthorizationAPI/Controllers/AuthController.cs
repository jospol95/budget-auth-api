using System;
using System.Threading.Tasks;
using AuthorizationAPI.Application;
using AuthorizationAPI.Application.Commands;
using AuthorizationAPI.Application.Exceptions;
using AuthorizationAPI.Application.Interfaces;
using AuthorizationAPI.Application.Models;
using AuthorizationAPI.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationAPI.Controllers
{
    [ApiController]
    [Route("authApi/[controller]")]
    
    public class AuthController : BaseController
    {
        private readonly ITokenService _tokenService;

        public AuthController(IMediator mediator, ITokenService tokenService) : base(mediator)
        {
            _tokenService = tokenService;
        }
        
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserCommand newUser)
        {
            try
            {
                var addedUserId = await _mediator.Send(newUser);
                var token = await GetTokenForUser(addedUserId);
                
                return Ok(new {token});
            }
            catch (EmailAlreadyRegisteredException emailAlreadyRegisteredException)
            {
                return Conflict(emailAlreadyRegisteredException.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserCommand loginUserCommand)
        {
            try
            {
                var existingUserId = await _mediator.Send(loginUserCommand);
                var token = await GetTokenForUser(existingUserId);

                return Ok(new {token});
            }
            catch (EmailNotRegisteredException emailNotRegisteredException)
            {
                return Unauthorized(emailNotRegisteredException.Message);
            }
            catch (EmailAndOrPasswordIncorrectException emailAndOrPasswordIncorrectException)
            {
                return Unauthorized(emailAndOrPasswordIncorrectException.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        private async Task<string> GetTokenForUser(string userId)
        {
            var userDto = await _mediator.Send(new GetUserDtoQuery(userId));
            return await _tokenService.GetTokenForUser(userDto);
        }
        
        
        // public async Task<IActionResult> LoginOrRegisterWithGoogle(string googleUserToken)
        // {
        //     return Ok();
        // }
        //
        // public async Task<IActionResult> LoginOrRegisterWithFacebook(string facebookUserToken)
        // {
        //     return Ok();
        // }

        // private string GenerateTokenForLoggedUser()
        // {
        //     return Ok("yes");
        // }

    }
}