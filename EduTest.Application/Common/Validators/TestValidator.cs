using EduTest.Application.Dtos.Tests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTest.Application.Common.Validators
{
    public class TestValidator : AbstractValidator<TestCreateDto>
    {
        public TestValidator()
        {
            RuleFor(a => a.TestCode)
                .NotEmpty()
                .WithMessage("Test code is required!")
                .Length(5,7);
        }
    }
}
