using System.Threading.Tasks;
using AuthorizationAPI.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class UserController : BaseController
    {
        public UserController(IMediator mediator) : base(mediator)
        {
            
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserDetails(string id)
        {
            var userDto = await _mediator.Send(new GetUserDtoQuery(id));
            if (userDto == null) return NotFound();

            return Ok(userDto);
        }
    }
}