using EduTest.Application.MediatR.Commands.StudentCommands;
using EduTest.DataAccess.Repositories.IRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTest.Application.MediatR.Handlers.StudentHandlers
{
    public class StudentDeleteHandler : IRequestHandler<StudentDeleteCommand, bool>
    {
        private readonly IStudentRepository _studentRepository;
        public StudentDeleteHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository; 
        }
        public async Task<bool> Handle(StudentDeleteCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if(request == null || request.Id<=0)
                    return false;

                await _studentRepository.DeleteAsync(request.Id);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
