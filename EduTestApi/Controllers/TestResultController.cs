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
                    res,
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

        [HttpGet("{studentId}")]
        public async ValueTask<IActionResult> TestResultGetByTestCode(long studentId)
        {
            var res=await _mediator.Send(new TestResultGetByStudentIdCommand() { StudentId= studentId });

            if (res == null)
                return BadRequest(new
                {
                    res,
                    StatusCode=404,
                    Error="Not found"
                });
            return Ok(res);
        }

        [HttpGet("testCode")]
        public async ValueTask<IActionResult> GetTestCode(string testCode)
        {
            var res = await _mediator.Send(new TestResultGetByTestCodeCommand() { TestCode = testCode });
            if (res == null)
                return NotFound();
            return Ok(res);

        }

        [HttpDelete("{id}")]
        public async ValueTask<IActionResult> Delete(long Id)
        {
            var res = await _mediator.Send(new TestResultDeleteCommand() { Id = Id });

            if(res)
                return NotFound();

            return Ok(res);
        }
     }
}
