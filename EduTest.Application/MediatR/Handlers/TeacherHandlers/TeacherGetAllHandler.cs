using EduTest.Application.MediatR.Commands.TeacherCommands;
using EduTest.Application.ViewModels;
using EduTest.DataAccess.Repositories.IRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTest.Application.MediatR.Handlers.TeacherHandlers
{
    public class TeacherGetAllHandler : IRequestHandler<TeacherGetAllQuery, List<TeacherViewModel>>
    {
        private readonly ITeacherRepository _repository;
        public TeacherGetAllHandler(ITeacherRepository repository)
        {
            _repository = repository;   
        }
        public async Task<List<TeacherViewModel>> Handle(TeacherGetAllQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var teachers = await _repository.GetAllAsync();
                if (teachers == null)
                    return new List<TeacherViewModel>();

                return teachers.Select(x => new TeacherViewModel()
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,  
                    PhoneNumber = x.PhoneNumber,
                }).ToList();
            }
            catch
            {
                return new List<TeacherViewModel>();
            }
        }
    }
}
