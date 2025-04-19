using EduTest.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTest.Domain.Models
{
    public class TestResult : BaseEntity
    {
        public string TestName { get; set; }=string.Empty;
        public long CorrectAnswer { get; set; }
        public long WrongAnswer { get; set; }
        public string TestCode { get; set; } = string.Empty;


        public long StudentId { get; set; }
        public Student Student { get; set; } 

        public long TestId { get; set; }
        public Test Test { get; set; }

    }
}
