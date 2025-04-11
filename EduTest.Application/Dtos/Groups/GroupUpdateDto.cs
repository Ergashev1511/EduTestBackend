using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTest.Application.Dtos.Groups
{
    public class GroupUpdateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Describtion { get; set; } = string.Empty;


        public long TeacherId { get; set; }
    }
}
