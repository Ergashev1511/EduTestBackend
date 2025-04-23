using EduTest.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTest.Application.Dtos.Tests
{
    public class TestCreateDto
    {
        public string Describtion { get; set; } = string.Empty;
        public string TestCode { get; set; } = string.Empty;
        public string AnswerKey { get; set; } = string.Empty;

        public string? FilePath { get; set; }
        public string? FileName { get; set; }
        public string? ContentType { get; set; }

        public long TeacherId { get; set; }
        public TestStatus TestStatus { get; set; }
    }
}
