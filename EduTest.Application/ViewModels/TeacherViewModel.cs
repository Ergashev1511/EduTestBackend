using EduTest.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTest.Application.ViewModels
{
    public class TeacherViewModel
    {
        public long Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        public List<StudentViewModel> StudentViewModels { get; set; } = new();
        public List<GroupViewModel> GroupViewModels { get; set; } = new();
        public List<TestViewModel> TestViewModels { get; set; } = new();
    }
}
