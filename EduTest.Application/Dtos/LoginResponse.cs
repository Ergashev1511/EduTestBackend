using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTest.Application.Dtos
{
    public class LoginResponse
    {
        public long Id { get; set; }
        public string Token { get; set; }=string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Result { get; set; }=string.Empty;
    }
}
