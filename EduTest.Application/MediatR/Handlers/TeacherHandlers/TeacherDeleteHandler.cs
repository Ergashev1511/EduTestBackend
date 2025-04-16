using EduTest.Application.MediatR.Commands.TeacherCommands;
using EduTest.DataAccess.Repositories.IRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTest.Application.MediatR.Handlers.TeacherHandlers
{
    public class TeacherDeleteHandler : IRequestHandler<TeacherDeleteCommand, bool>
    {
        private readonly ITeacherRepository _repository;
        public TeacherDeleteHandler(ITeacherRepository repository)
        {
            _repository = repository;   
        }
        public async Task<bool> Handle(TeacherDeleteCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null || request.Id<=0)
                    return false;

                await _repository.DeleteAsync(request.Id);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
