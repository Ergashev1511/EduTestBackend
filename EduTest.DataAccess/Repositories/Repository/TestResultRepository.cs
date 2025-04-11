using EduTest.DataAccess.DBContext;
using EduTest.DataAccess.Repositories.IRepository;
using EduTest.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTest.DataAccess.Repositories.Repository
{
    public class TestResultRepository : ITestResultRepository
    {
        private readonly AppDbContext _dbContext;
        public TestResultRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<TestResult> AddAsync(TestResult result)
        {
            try
            {
                await _dbContext.TestResults.AddAsync(result);
                await _dbContext.SaveChangesAsync();
                return result;
            }
            catch
            {
                return new TestResult(); 
            }
        }

        public async Task<List<TestResult>> GetAllAsync()
        {
            try
            {
                return await _dbContext.TestResults
                        .Include(tr => tr.Student)
                        .Include(tr => tr.Test)
                        .ToListAsync();
            }
            catch
            {
                return new List<TestResult>();
            }
        }

        public async Task<TestResult> GetByIdAsync(long id)
        {
            try
            {
                var testresult = await _dbContext.TestResults
                    .Include(tr => tr.Student)
                    .Include(tr => tr.Test)
                    .FirstOrDefaultAsync(a => a.Id == id);

                return testresult!;
            }
            catch
            {
                return new();
            }
        }
    }
}
