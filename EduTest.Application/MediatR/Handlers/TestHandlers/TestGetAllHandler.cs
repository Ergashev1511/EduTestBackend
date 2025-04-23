using EduTest.Application.MediatR.Commands.TestCommands;
using EduTest.Application.ViewModels;
using EduTest.DataAccess.Repositories.IRepository;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTest.Application.MediatR.Handlers.TestHandlers
{
    public class TestGetAllHandler : IRequestHandler<TestGetAllQuery, List<TestViewModel>>
    {
        private readonly ITestRepository _repository;

        public TestGetAllHandler(ITestRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<TestViewModel>> Handle(TestGetAllQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var test = await _repository.GetAllAsync();
                if (test == null)
                    return new List<TestViewModel>();

                return test.Where(s=>s.TestStatus==request.testStatus).Select(a => new TestViewModel()
                {
                    Id = a.Id,
                    Name = a.Name,
                    Describtion = a.Describtion,
                    AnswerKey = a.AnswerKey,
                    TestCode = a.TestCode,
                    TeacherId = a.TeacherId,
                    TeacherFullName = a.Teacher.FirstName + " " + a.Teacher.LastName,
                    TestStatus = a.TestStatus,
                }).ToList();
            }
            catch
            {
                return new List<TestViewModel> { };
            }
        }
    }
}
