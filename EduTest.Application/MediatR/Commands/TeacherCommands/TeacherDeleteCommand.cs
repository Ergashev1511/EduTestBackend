using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTest.Application.MediatR.Commands.TeacherCommands
{
    public class TeacherDeleteCommand : IRequest<bool>
    {
        public long Id { get; set; }
    }
}
