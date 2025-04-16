using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTest.Application.MediatR.Commands.StudentCommands
{
    public class StudentDeleteCommand : IRequest<bool>
    {
        public long Id { get; set; }
    }
}
