using EduTest.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTest.DataAccess.Repositories.IRepository
{
    public interface IStudentRepository
    {
        Task<Student> AddAsync(Student student);
        Task<Student> UpdateAsync(long Id, Student student);
        Task<bool> DeleteAsync(long Id);
        Task<List<Student>> GetAllAsync();
        Task<Student> GetByIdAsync(long Id);
        Task<Student> GetByUserNameAsync(string userName);
    }
}
