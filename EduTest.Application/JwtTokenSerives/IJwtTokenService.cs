using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTest.Application.JwtTokenSerives
{
    public interface IJwtTokenService
    {
        public string GenerateToken(string username);
    }
}
