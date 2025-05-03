using EduTest.Application.MediatR.Commands.StudentCommands;
using EduTest.Application.ViewModels;
using EduTest.DataAccess.Repositories.IRepository;
using EduTest.DataAccess.Repositories.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTest.Application.MediatR.Handlers.StudentHandlers
{
    public class StudentGetByIdHandler : IRequestHandler<StudentGetByIdQuery, StudentViewModel>
    {
        private readonly IStudentRepository _repository;
        public StudentGetByIdHandler(IStudentRepository repository)
        {
            _repository= repository;
        }
        public async Task<StudentViewModel> Handle(StudentGetByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var student = await _repository.GetByIdAsync(request.Id);

                if (student == null)
                    return new StudentViewModel();


                var studentView = new StudentViewModel
                {
                    Id = student.Id,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    PhoneNumber = student.PhoneNumber,

                    GroupId = student.GroupId,
                    GroupName = student.Group?.Name,

                    TeacherId = student.Teacher?.Id ?? 0,
                    TeacherFullName = (student.Teacher != null)
                          ? student.Teacher.FirstName + " " + student.Teacher.LastName
                          : ""
                };

                return studentView;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
