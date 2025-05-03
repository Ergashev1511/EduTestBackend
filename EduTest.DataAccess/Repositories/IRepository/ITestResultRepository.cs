using EduTest.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTest.DataAccess.Repositories.IRepository
{
    public interface ITestResultRepository
    {
        Task<TestResult> AddAsync(TestResult result);
        Task<List<TestResult>> GetAllAsync();
        Task<TestResult> GetByIdAsync(long id);
        Task<bool> DeleteAsync(long id);
    }
}
