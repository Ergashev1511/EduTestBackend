using EduTest.Domain.Models.Base;
using EduTest.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTest.Domain.Models
{
    public class Student : BaseEntity
    {
        public string  FirstName { get; set; }=string.Empty;
        public string LastName { get; set; } =string.Empty;
        public string PhoneNumber { get; set; }=string.Empty ;
        public string UserName { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string PasswordSalt { get; set; } = string.Empty;

        public long GroupId { get; set; }
        public Group Group { get; set; }
        public long TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public Role Role { get; set; }

        public List<Test> Tests { get; set; } 
        public List<TestResult> TestResults { get; set; }
    }
}
