﻿using EduTest.Application.Dtos.Teachers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTest.Application.MediatR.Commands.AuthCommands
{
    public class TeacherRegisterCommand : IRequest<string>
    {
        public TeacherCreateDto TeacherCreateDto { get; set; } = new();
    }
}
