using EduTest.Application.Dtos.Tests;
using EduTest.Application.MediatR.Commands.TestCommands;
using EduTest.Domain.Models.Enums;
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
        private readonly IWebHostEnvironment _webHostEnvironment;
        public TestController(IMediator mediator,IWebHostEnvironment webHostEnvironment)
        {
            _mediator = mediator;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost("upload")]
        public async ValueTask<IActionResult> TestCreate(IFormFile file, [FromForm] string description, [FromForm] string answerKey, [FromForm] long teacherId)
        {

            if (file == null)
                return BadRequest("File topilmadi.");

            var allowedExtensions = new[] { ".pdf", ".doc", ".docx", ".txt" };
            var allowedContentTypes = new[] { "application/pdf", "application/msword", "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "text/plain" };
            var fileExtension = Path.GetExtension(file.FileName).ToLower();

            if (!allowedExtensions.Contains(fileExtension))
                return BadRequest("Faqat pdf, doc, docx va txt fayllarni yuklash mumkin.");


            var uploadspath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
            if(!Directory.Exists(uploadspath))
                Directory.CreateDirectory(uploadspath);


            var originalName = Path.GetFileNameWithoutExtension(file.FileName);
            var extension = Path.GetExtension(file.FileName);
            var safeFileName = $"{Guid.NewGuid()}_{originalName}{extension}";
            var filepath = Path.Combine(uploadspath, safeFileName);
            using (var stream=new FileStream(filepath,FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var dto = new TestCreateDto()
            {
                FileName = safeFileName,
                Describtion = description,
                AnswerKey = answerKey,
                TeacherId = teacherId,
                ContentType=file.ContentType,
                FilePath = $"{uploadspath}/{safeFileName}",
                TestStatus=TestStatus.NoStarted,

            };

            var command = new TestCreateCommand()
            {
                TestCreateDto = dto
            };

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

        [HttpGet("download/{fileName}")]
          public IActionResult DownloadFile(string fileName)
          {
              var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
              var filePath = Path.Combine(uploadPath, fileName);
          
              if (!System.IO.File.Exists(filePath))
                  return NotFound("Fayl topilmadi.");
          
              var fileBytes = System.IO.File.ReadAllBytes(filePath);
              var contentType = "application/octet-stream"; // umumiy, yoki aniqlab qo‘ysangiz ham bo'ladi
          
              return File(fileBytes, contentType, fileName);
          }

        [HttpGet("GetAll")]
        public async ValueTask<IActionResult> GetAllTest(TestStatus testStatus)
        {
            var res = await _mediator.Send(new TestGetAllQuery() { testStatus=testStatus});
            if (res == null)
                return NotFound(new
                {
                    Error="Not found"
                });
            return Ok(res);
        }

        [HttpDelete("delete")]
        public async ValueTask<IActionResult> TestDelete(long Id)
        {
            var res=await _mediator.Send(new TestDeleteCommmand() { Id=Id});
            if (res == false)
                return BadRequest(new
                {
                    StatusCode=404,
                    Error="Delete failed"
                });

            return Ok(res);

        }

        [HttpPatch("status-update")]
        public async ValueTask<IActionResult> TestStatusUpdate(long Id, TestStatus testStatus)
        {
            var res = await _mediator.Send(new TestStatusUpdateCommand() { Id = Id, testStatus = testStatus });
            if (res == false)
                return BadRequest(new
                {
                    res,
                    StatusCode = 404,
                    Error = "Status code update failed"
                });
            return Ok(res);
        }

        [HttpPost("test-check")]
        public async ValueTask<IActionResult> TestCheck(TestCheckCommand command)
        {
            var res=await _mediator.Send(command);
            if(res==null)
                return BadRequest();
            return Ok(res);
        }
    }


}

