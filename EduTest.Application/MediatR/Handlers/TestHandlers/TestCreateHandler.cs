using EduTest.Application.Dtos.Tests;
using EduTest.Application.MediatR.Commands.TestCommands;
using EduTest.DataAccess.Repositories.IRepository;
using EduTest.Domain.Models;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTest.Application.MediatR.Handlers.TestHandlers
{
    public class TestCreateHandler : IRequestHandler<TestCreateCommand, bool>
    {
        private readonly ITestRepository _repository;
        private readonly IValidator<TestCreateDto> _validator;

        public TestCreateHandler(ITestRepository repository,IValidator<TestCreateDto> validator)
        {
            _repository = repository;
            _validator = validator;
        }
        public async Task<bool> Handle(TestCreateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var ValidateRes=_validator.Validate(request.TestCreateDto);
                if (!ValidateRes.IsValid)
                    throw new ValidationException(ValidateRes.Errors.First().ErrorMessage);

                var test = new Test()
                {
                    Name=request.TestCreateDto.FileName,
                    Describtion=request.TestCreateDto.Describtion,
                    TestCode=$"#{CodeRandom()}",
                    AnswerKey=request.TestCreateDto.AnswerKey,
                    FilePath=request.TestCreateDto.FilePath,
                    ContentType=request.TestCreateDto.ContentType,
                    TeacherId =request.TestCreateDto.TeacherId,  
                    TestStatus=request.TestCreateDto.TestStatus,
                };

                await _repository.AddAsync(test);
                return true;
            }
            catch
            {
                return false;
            }
        }

       private long CodeRandom()
        {
            var random = new Random().Next(1000,10000);
            return random;
        }
    }
}
