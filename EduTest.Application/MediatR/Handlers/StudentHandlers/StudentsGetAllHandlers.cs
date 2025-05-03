using EduTest.Application.MediatR.Commands.StudentCommands;
using EduTest.Application.ViewModels;
using EduTest.DataAccess.Repositories.IRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTest.Application.MediatR.Handlers.StudentHandlers
{
    public class StudentsGetAllHandlers : IRequestHandler<StudentsGetAllCommands, List<StudentViewModel>>
    {
        private readonly IStudentRepository _studentRepository;
        public StudentsGetAllHandlers(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        public async Task<List<StudentViewModel>> Handle(StudentsGetAllCommands request, CancellationToken cancellationToken)
        {
            try
            {
                var students = await _studentRepository.GetAllAsync();

                if (students == null)
                    return new List<StudentViewModel> { };


                var studentView = students.Select(a => new StudentViewModel()
                {
                    Id = a.Id,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    PhoneNumber = a.PhoneNumber,

                    GroupId = a.GroupId,
                    GroupName = a.Group.Name,

                    TeacherFullName = a.Teacher.FirstName + " " + a.Teacher.LastName,
                    TeacherId = a.Teacher.Id,


                }).ToList();
                return studentView;
            }
            catch
            {
                return new List<StudentViewModel>();
            }
        }
    }
}
