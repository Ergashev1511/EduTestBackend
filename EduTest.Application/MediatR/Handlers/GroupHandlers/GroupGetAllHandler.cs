using EduTest.Application.MediatR.Commands.GroupCommands;
using EduTest.Application.ViewModels;
using EduTest.DataAccess.Repositories.IRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTest.Application.MediatR.Handlers.GroupHandlers
{
    public class GroupGetAllHandler : IRequestHandler<GroupGetAllQuery, List<GroupViewModel>>
    {
       private readonly IGroupRepository _groupRepository;
        public GroupGetAllHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }
        public async Task<List<GroupViewModel>> Handle(GroupGetAllQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var groups = await _groupRepository.GetAllAsync();
                if (groups == null)
                    return new List<GroupViewModel> { };

                var groupView = groups.Select(g => new GroupViewModel()
                {
                    Id = g.Id,
                    Name = g.Name,
                    Describtion = g.Describtion,

                    TeacherId = g.TeacherId,
                    TeacherFullName = g.Teacher.FirstName + " " + g.Teacher.LastName,

                    StudentViewModels = g.Students?.Select(x => new StudentViewModel()
                    {
                        Id=x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        PhoneNumber = x.PhoneNumber,

                        TeacherFullName=x.Teacher.FirstName+" "+x.Teacher.LastName,
                        TeacherId=x.Teacher.Id,

                    }).ToList() ?? new List<StudentViewModel>()
                   
                }).ToList();
                return groupView;
            }
            catch
            {
                return new List<GroupViewModel> { };
            }
        }
    }
}
