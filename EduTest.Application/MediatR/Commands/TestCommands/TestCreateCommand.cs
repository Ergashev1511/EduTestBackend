using EduTest.Application.Dtos.Tests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTest.Application.MediatR.Commands.TestCommands
{
    public class TestCreateCommand : IRequest<bool>
    {
        public TestCreateDto TestCreateDto { get; set; } = new();
    }
}
