﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTest.Application.Common.Exceptions
{
    public class ValidatorException : Exception
    {
        public ValidatorException(string message) : base(message) { }
    }
}
