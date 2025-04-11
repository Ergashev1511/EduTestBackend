using EduTest.Domain.Models.Enums;
using EduTest.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTest.Application.ViewModels
{
    public class TestViewModel
    {
        public string Name { get; set; } = string.Empty;
        public string Describtion { get; set; } = string.Empty;
        public string TestCode { get; set; } = string.Empty;
        public string AnswerKey { get; set; } = string.Empty;

        public long StudentId { get; set; }
        public string StudentFullName { get; set; } =string.Empty;
        public long TeacherId { get; set; }
        public string TeacherFullName { get; set; } = string.Empty;

        public List<TestResultViewModel> TestResultViewModels { get; set; } = new();
        public TestStatus TestStatus { get; set; }
    }
}
