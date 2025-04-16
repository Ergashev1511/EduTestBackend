using EduTest.Application.MediatR.Commands.GroupCommands;
using EduTest.DataAccess.Repositories.IRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTest.Application.MediatR.Handlers.GroupHandlers
{
    public class GroupDeleteHandler : IRequestHandler<GroupDeleteCommand, bool>
    {
        private readonly IGroupRepository _groupRepository;

        public GroupDeleteHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<bool> Handle(GroupDeleteCommand request, CancellationToken cancellationToken)
        {
            try
            {

                try
                {
                    if (request.Id <= 0)
                        return false;
                    else
                        await _groupRepository.DeleteAsync(request.Id);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
