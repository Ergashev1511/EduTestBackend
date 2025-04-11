using EduTest.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTest.Application.ViewModels
{
    public class StudentViewModel
    {
        public long Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

       // public GroupViewModel GroupViewModel { get; set; } = new();  
        public long GroupId { get; set; }
        public string GroupName { get; set; }=string.Empty;


       // public TeacherViewModel TeacherViewModel { get; set; } = new();
        public long TeacherId  { get; set; }
        public string TeacherFullName { get; set; }=string.Empty;

        public List<TestViewModel> TestViewModels { get; set; } = new();
        public List<TestResultViewModel> TestResultViewModels { get; set; } = new();
    }
}
