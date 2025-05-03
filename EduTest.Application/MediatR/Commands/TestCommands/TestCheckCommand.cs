using EduTest.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTest.Application.MediatR.Commands.TestCommands
{
    public class TestCheckCommand : IRequest<CheckTest>
    {
        public string TestCode { get; set; }
        public string Answer { get; set; }
    }
}
