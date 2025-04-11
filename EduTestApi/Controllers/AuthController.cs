using EduTest.Application.MediatR.Commands.AuthCommands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EduTestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediatR;
        public AuthController(IMediator mediator)
        {
            _mediatR = mediator;
        }

        [HttpPost("register")]
        public async ValueTask<IActionResult> TeacherRegister([FromBody] TeacherRegisterCommand command)
        {
            var res=await _mediatR.Send(command);

            if(res.StartsWith("Registration failed"))
                return BadRequest(res);

            return Ok(new { Token = res });

        }

        [HttpGet("teacher-login")]
        public async ValueTask<IActionResult> TeacherLogin(TeacherLoginCommand command)
        {
            var res = await _mediatR.Send(command);
            if(res.StartsWith("Registration failed"))
                return BadRequest(res);

            return Ok(new { Token = res });
        }

        [HttpGet("student-login")]
        public async ValueTask<IActionResult> StudentLogin(StudentLoginCommand command)
        {
            var res= await _mediatR.Send(command);

            if(res.StartsWith("Registration failed"))
                return BadRequest(res);

            return Ok(new { Token = res });
        }
    }
}
