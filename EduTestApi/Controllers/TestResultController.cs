using EduTest.Application.MediatR.Commands.TestResultCommands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduTestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestResultController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TestResultController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async ValueTask<IActionResult> TestResultCreate([FromBody] TestResultCreateCommand command)
        {
            var res = await _mediator.Send(command);

            if (res == false)
                return BadRequest(new
                {
                    StatusCode = 404,
                    Error = "Creat failed"
                });

            return Ok(new
            {
                res,
                StatusCode = 200,
            });
        }

        [HttpGet("get-all")]
        public async ValueTask<IActionResult> TestResultGetAll()
        {
            var res = await _mediator.Send(new TestResultGetAllQuery());
            if (res == null)
                return NotFound(new
                {
                    StatusCode = 404,
                    Error = "Not found"
                });

            return Ok(new
            {
                res,
                StatusCode = 200,
            });
        }

        [HttpGet("{test-code}")]
        public async ValueTask<IActionResult> TestResultGetByTestCode(string testCode)
        {
            var res=await _mediator.Send(new TestResultGetByTestCodeCommand() { TestCode = testCode });

            if (res == null)
                return BadRequest(new
                {
                    StatusCode=404,
                    Error="Not found"
                });
            return Ok(res);
        }
     }
}
