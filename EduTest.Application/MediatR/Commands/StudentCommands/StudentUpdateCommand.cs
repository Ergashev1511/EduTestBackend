using EduTest.Application.Dtos.Students;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTest.Application.MediatR.Commands.StudentCommands
{
    public class StudentUpdateCommand : IRequest<bool>
    {
        public long Id { get; set; }
        public StudentUpdateDto StudentUpdateDto { get; set; } = new();
    }
}
