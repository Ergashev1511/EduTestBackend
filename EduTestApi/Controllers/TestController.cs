using EduTest.Application.MediatR.Commands.TestCommands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduTestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TestController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async ValueTask<IActionResult> TestCreate([FromBody] TestCreateCommand command)
        {
            var res=await _mediator.Send(command);
            if (res)
            {
                return Ok(new
                {
                    StatusCode = 200,
                    Result = "Succes"
                });
            }
            else
                return BadRequest(new
                {
                    StatusCode = 404,
                    Result = "Create failed"
                });
        }
    }
}
