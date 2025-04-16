using EduTest.Application.Dtos.Groups;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTest.Application.MediatR.Commands.GroupCommands
{
    public class GroupUpdateCommand : IRequest<bool>
    {
        public long Id { get; set; }
        public GroupUpdateDto GroupUpdateDto { get; set; } = new();
    }
}
