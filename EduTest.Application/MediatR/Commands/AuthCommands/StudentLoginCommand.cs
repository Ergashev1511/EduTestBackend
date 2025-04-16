using EduTest.Application.Dtos;
using EduTest.Application.Dtos.Students;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTest.Application.MediatR.Commands.AuthCommands
{
    public class StudentLoginCommand : IRequest<LoginRequst>
    {
        public StudentLoginDto StudentLoginDto { get; set; } = new();
    }
}
