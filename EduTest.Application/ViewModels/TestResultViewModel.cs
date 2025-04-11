using EduTest.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTest.Application.ViewModels
{
    public class TestResultViewModel
    {
        public string TestName { get; set; } = string.Empty;
        public long CorrectAnswer { get; set; }
        public long WrongAnswer { get; set; }
        public string TestCode { get; set; } = string.Empty;


        public long StudentId { get; set; }
        public string StudentFullName { get; set; } = string.Empty ;

        public long TestId { get; set; }
        public string AnswerKey { get; set; } = string.Empty;
    }
}
