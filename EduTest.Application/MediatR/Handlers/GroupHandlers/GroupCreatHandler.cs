using EduTest.Application.Dtos.Groups;
using EduTest.Application.MediatR.Commands.GroupCommands;
using EduTest.DataAccess.Repositories.IRepository;
using EduTest.Domain.Models;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTest.Application.MediatR.Handlers.GroupHandlers
{
    public class GroupCreatHandler : IRequestHandler<GroupCreateCommand, bool>
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IValidator<GroupCreateDto> _validator;

        public GroupCreatHandler(IGroupRepository groupRepository, IValidator<GroupCreateDto> validator)
        {
            _groupRepository = groupRepository;
            _validator = validator;
        }
        public async Task<bool> Handle(GroupCreateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var validateRes = _validator.Validate(request.GroupCreateDto);
                if (!validateRes.IsValid)
                    throw new ValidationException(validateRes.Errors.First().ErrorMessage);

                var group = new Group()
                {
                    Name=request.GroupCreateDto.Name,
                    Describtion=request.GroupCreateDto.Describtion,
                    TeacherId=request.GroupCreateDto.TeacherId
                };

                await _groupRepository.AddAsync(group);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
