using EduTest.Application.Dtos.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTest.Application.MediatR.Commands.TestCommands
{
    public class TestUpdateCommand
    {
        public long Id { get; set; }
        public TestUpdateDto TestUpdateDto { get; set; } = new();
    }
}
