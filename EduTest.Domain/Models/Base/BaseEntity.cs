using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EduTest.Domain.Models.Base
{
    public abstract class BaseEntity
    {
        public long Id { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
