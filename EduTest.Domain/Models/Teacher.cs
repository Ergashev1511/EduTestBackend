using EduTest.Domain.Models.Base;
using EduTest.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTest.Domain.Models
{
    public class Teacher : BaseEntity
    {
        public string FirstName { get; set; }=string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; }=string.Empty ;
        public string UserName { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string PasswordSalt { get; set; } = string.Empty;
        public Role Role { get; set; }

        public List<Student> Students { get; set; }
        public List<Group>  Groups { get; set; } 
        public List<Test> Tests { get; set; } 
    }
}
