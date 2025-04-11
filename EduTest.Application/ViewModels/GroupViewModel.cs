using EduTest.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTest.Application.ViewModels
{
    public class GroupViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Describtion { get; set; } = string.Empty;


        public long TeacherId { get; set; }
        public string TeacherFullName { get; set; } = string.Empty; 
        public List<StudentViewModel> StudentViewModels { get; set; } = new();
    }
}
