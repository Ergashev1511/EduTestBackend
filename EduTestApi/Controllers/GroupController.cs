using EduTest.Application.MediatR.Commands.GroupCommands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduTestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IMediator _mediator;
        public GroupController(IMediator mediator)
        {
            _mediator = mediator;   
        }

        [HttpPost("create")]
        public async ValueTask<IActionResult> GroupCreate([FromBody] GroupCreateCommand command)
        {
            var res= await _mediator.Send(command);
            if(res)
                return Ok(new
                {
                    StatusCode = 200,
                    Result = "Succes"
                });
            else
                return BadRequest(new
                {
                    StatusCode = 404,
                    Result = "Create failed"
                });
        }

        [HttpDelete("delete")]
        public async ValueTask<IActionResult> GroupDelete(long Id)
        {
            var res= await _mediator.Send(new  GroupDeleteCommand { Id = Id });

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
                    Result = "Delete failed"
                });
        }

        [HttpPut("{id}")]
        public async ValueTask<IActionResult> GroupUpdate([FromBody] GroupUpdateCommand command)
        {
            var res=await _mediator.Send(command);
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
                    Result = "Update failed"
                });
        }

        [HttpGet("GetAll")]
        public async ValueTask<IActionResult> GroupGetAll()
        {
            var res = await _mediator.Send(new GroupGetAllQuery());
            if(res==null)
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
