using EduTest.Application.MediatR.Commands.GroupCommands;
using EduTest.DataAccess.Repositories.IRepository;
using EduTest.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTest.Application.MediatR.Handlers.GroupHandlers
{
    public class GroupUpdateHandler : IRequestHandler<GroupUpdateCommand, bool>
    {
        private readonly IGroupRepository _groupRepository;
        public GroupUpdateHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository; 
        }
        public async Task<bool> Handle(GroupUpdateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if(request == null || request.Id<=0)
                    return false;

                var group = new Group()
                {
                    Name=request.GroupUpdateDto.Name,
                    Describtion=request.GroupUpdateDto.Describtion,
                    TeacherId=request.GroupUpdateDto.TeacherId,
                };

                await _groupRepository.UpdateAsync(request.Id,group);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
