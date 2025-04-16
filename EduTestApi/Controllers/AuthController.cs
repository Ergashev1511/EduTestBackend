using EduTest.Application.JwtTokenSerives;
using EduTest.Application.MediatR.Commands.AuthCommands;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EduTestApi.Controllers
{
    [EnableCors("MyCorsImpelementationPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediatR;
        private readonly IJwtTokenService _jwtTokenService;
        public AuthController(IMediator mediator,IJwtTokenService jwtTokenService)
        {
            _mediatR = mediator;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost("register")]
        public async ValueTask<IActionResult> TeacherRegister([FromBody] TeacherRegisterCommand command)
        {
            var res=await _mediatR.Send(command);

            if(res.StartsWith("Registration failed"))
                return BadRequest(res);

            return Ok(new { Token = res });

        }

        [HttpPost("teacher-login")]
        public async ValueTask<IActionResult> TeacherLogin([FromBody]TeacherLoginCommand command)
        {
            var res = await _mediatR.Send(command);
            if(res.Result.StartsWith("Login failed"))
                return BadRequest(res);

            return Ok(new 
            { 
                Token = res,
                UserName = res.Username,
                Role = res.Role,
            });
        }

        [HttpPost("student-login")]
        public async ValueTask<IActionResult> StudentLogin([FromBody]StudentLoginCommand command)
        {
            var res= await _mediatR.Send(command);

            if(res.Result.StartsWith("Registration failed"))
                return BadRequest(res);

            return Ok(new
            { 
                Token = res ,
                UserName=res.Username,
                Role=res.Role,
            });
        }

        [HttpGet("check-token")]
        public IActionResult CheckToken([FromHeader] string token)
        {
            var result = _jwtTokenService.ValidateToken(token);
            if (result == null)
                return Unauthorized("Token yaroqsiz yoki muddati tugagan.");

            var username = result.Identity.Name;
            var role = result.FindFirst(ClaimTypes.Role)?.Value;

            return Ok(new { Username = username, Role = role });
        }


    }
}
