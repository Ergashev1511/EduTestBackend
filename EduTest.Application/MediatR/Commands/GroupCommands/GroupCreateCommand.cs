using EduTest.Application.Dtos.Groups;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTest.Application.MediatR.Commands.GroupCommands
{
    public class GroupCreateCommand : IRequest<bool>
    {
        public GroupCreateDto GroupCreateDto { get; set; } = new();
    }
}
