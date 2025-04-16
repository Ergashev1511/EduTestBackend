using EduTest.Application.Dtos.Students;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTest.Application.MediatR.Commands.StudentCommands
{
    public class StudentCreateCommand : IRequest<bool>  
    {
        public StudentCreateDto StudentCreateDto { get; set; } = new();
    }

}
