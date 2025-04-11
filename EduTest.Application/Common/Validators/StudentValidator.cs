using EduTest.Application.Dtos.Students;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTest.Application.Common.Validators
{
    public class StudentValidator : AbstractValidator<StudentCreateDto>
    {
        public StudentValidator()
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

            RuleFor(a => a.PhoneNumber)
               .NotNull()
               .NotEmpty()
               .Matches(@"^(\+998|998|8)?(9[0-9]{1})(\d{7})$")
               .WithMessage("Telefon raqami noto‘g‘ri formatda");
        }
    }
}
