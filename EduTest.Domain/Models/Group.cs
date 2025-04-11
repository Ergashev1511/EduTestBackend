using EduTest.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTest.Domain.Models
{
    public class Group : BaseEntity
    {
        public string Name { get; set; }=string.Empty;
        public string Describtion { get; set; }=string.Empty ;


        public long TeacherId { get; set; }
        public Teacher Teacher { get; set; } = new();
        public List<Student> Students { get; set; } = new();
    }

}
