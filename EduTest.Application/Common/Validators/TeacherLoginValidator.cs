using EduTest.Application.Dtos.Teachers;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTest.Application.Common.Validators
{
    public class TeacherLoginValidator : AbstractValidator<TeacherLoginDto>
    {
        public TeacherLoginValidator()
        {
            RuleFor(a => a.UserName)
             .NotNull()
             .NotEmpty()
             .Matches("^[a-zA-Z0-9_]*$").WithMessage("Username faqat harflar, raqamlar yoki pastki chiziq (_) dan iborat bo‘lishi kerak")
             .WithMessage("User Name is required!");

            RuleFor(a => a.Password)
                .NotNull()
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(8);


        }
    }
}
