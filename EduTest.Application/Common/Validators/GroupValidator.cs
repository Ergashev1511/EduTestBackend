using EduTest.Application.Dtos.Groups;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTest.Application.Common.Validators
{
    public class GroupValidator : AbstractValidator<GroupCreateDto>
    {
        public GroupValidator()
        {
            RuleFor(a => a.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Group Name is required!");
        }
    }
}
