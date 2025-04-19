using EduTest.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTest.Application.MediatR.Commands.AuthCommands
{
    public class LoginCommand : IRequest<LoginResponse>
    {
        public LoginDto LoginDto { get; set; } = new();
    }
}
