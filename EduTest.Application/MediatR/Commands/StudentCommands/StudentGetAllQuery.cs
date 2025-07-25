﻿using EduTest.Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTest.Application.MediatR.Commands.StudentCommands
{
    public class StudentGetAllQuery : IRequest<List<StudentViewModel>>
    {
        public string GroupName { get; set; }=string.Empty;
    }
}
