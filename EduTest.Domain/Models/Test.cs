using EduTest.Domain.Models.Base;
using EduTest.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTest.Domain.Models
{
    public class Test : BaseEntity
    {
        public string Name { get; set; }=string.Empty;
        public string Describtion { get; set; }=string.Empty ;
        public string TestCode { get; set; } = string.Empty;
        public string AnswerKey { get; set; } = string.Empty;

        public long StudentId { get; set; }
        public Student Student { get; set; } = new();
        public long TeacherId { get; set; }
        public Teacher Teacher { get; set; }=new();

        public List<TestResult> TestResults { get; set; }=new();
        public TestStatus TestStatus { get; set; }
    }
}
