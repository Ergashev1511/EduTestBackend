using EduTest.Application.MediatR.Commands.StudentCommands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduTestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public StudentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async ValueTask<IActionResult> StudentCreate([FromBody] StudentCreateCommand command)
        {
            var res= await _mediator.Send(command);

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
                    Result = " Create Failed"
                });
        }

        [HttpDelete("delete")]
        public async ValueTask<IActionResult> StudentDelete(long Id)
        {
            var res=await _mediator.Send(new StudentDeleteCommand { Id=Id});

            if (res)
            {
                return Ok(new
                {
                    StatusCode = 200,
                    Result = "Succes"
                });
            }
            else
                return NotFound(new
                {
                    StatusCode = 404,
                    Result = "Not found"
                });
        }

        [HttpPut("{id}")]
        public async ValueTask<IActionResult> StudentUpdate([FromBody] StudentUpdateCommand command)
        {
            var res = await _mediator.Send(command);
            if(res)
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
                    Result = "Updated Failed"
                });
        }

        [HttpGet("GetAll")]
        public async ValueTask<IActionResult> StudentGetAll()
        {
            var res =await _mediator.Send(new StudentGetAllQuery());
            if (res != null)
            {
                return Ok(res);
            }

            else 
                return NotFound(new
                {
                    StatusCode = 404,
                    Result = "Not found"
                });
        }
    }
}
