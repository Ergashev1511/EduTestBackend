using EduTest.Application.Dtos.Teachers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTest.Application.MediatR.Commands.AuthCommands
{
    public class TeacherLoginCommand : IRequest<string>
    {
        public TeacherLoginDto TeacherLoginDto { get; set; } = new();
    }
}
