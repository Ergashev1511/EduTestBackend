using EduTest.Application.MediatR.Commands.TeacherCommands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduTestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TeacherController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpDelete("delete")]
        public async ValueTask<IActionResult> DeleteTeacher(long Id)
        {
            var res=await _mediator.Send(new TeacherDeleteCommand { Id = Id });

            if(!res)
            {
                return NotFound(new
                {
                    StatusCode=404,
                    Error="Not nound"
                });
            }
            return Ok(new
            {
                StatusCode=200,
                TeacherId=Id
            });
        }

        [HttpGet("getAll")]
        public async ValueTask<IActionResult> TeacherGetAll()
        {
            var res = await _mediator.Send(new TeacherGetAllQuery());
            if (res == null)
                return NotFound(new
                {
                    StatusCode=404,
                    Error="Not found"
                });
            return Ok(res); 
        }
    }
}
