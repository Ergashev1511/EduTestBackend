﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTest.Application.Dtos
{
    public class LoginDto
    {
        public string UserName { get; set; }=string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
