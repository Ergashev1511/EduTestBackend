﻿using EduTest.Application.ViewModels;
using EduTest.Domain.Models.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTest.Application.MediatR.Commands.TestCommands
{
    public class TestGetAllQuery : IRequest<List<TestViewModel>>
    {
        public TestStatus testStatus { get; set; }
    }
}
