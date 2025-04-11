using EduTest.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTest.DataAccess.Repositories.IRepository
{
    public interface ITeacherRepository
    {
        Task<Teacher> AddAsync(Teacher teacher);
        Task<Teacher> UpdateAsync(long Id, Teacher teacher);
        Task<bool> DeleteAsync(long Id);
        Task<List<Teacher>> GetAllAsync();
        Task<Teacher> GetByIdAsnc(long Id);
        Task<Teacher> GetByUserNameAsync(string userName);

    }
}
