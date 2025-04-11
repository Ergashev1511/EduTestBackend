using EduTest.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTest.DataAccess.Repositories.IRepository
{
    public interface ITestRepository
    {
        Task<Test> AddAsync(Test test);
        Task<Test> UpdateAsync(long Id, Test test);
        Task<bool> DeleteAsync(long Id);
        Task<List<Test>> GetAllAsync();
        Task<Test> GetByIdAsync(long Id);
    }
}
