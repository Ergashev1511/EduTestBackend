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
    public class TestRepository : ITestRepository
    {
        private readonly AppDbContext _dbContext;
        public TestRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Test> AddAsync(Test test)
        {
            try
            {
                await _dbContext.Tests.AddAsync(test);
                await _dbContext.SaveChangesAsync();
                return test;
            }
            catch
            {
                return new Test();
            }
        }

        public async Task<bool> DeleteAsync(long Id)
        {
            try
            {
                var test = await _dbContext.Tests.FirstOrDefaultAsync(a => a.Id == Id);
                _dbContext.Tests.Remove(test!);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Test>> GetAllAsync()
        {
            try
            {
                return await _dbContext.Tests
                           .Include(t => t.Teacher)
                           .Include(t => t.Student)
                           .Include(t => t.TestResults)
                               .ThenInclude(tr => tr.Student)
                           .ToListAsync();
            }
            catch
            {
                return new List<Test>();
            }
        }

        public async Task<Test> GetByIdAsync(long Id)
        {
            try
            {
                var test = await _dbContext.Tests
                    .Include(t => t.Teacher)
                    .Include(t => t.Student)
                    .Include(t => t.TestResults)
                    .ThenInclude(tr => tr.Student)
                    .FirstOrDefaultAsync(a => a.Id == Id);
                return test!;
            }
            catch
            {
                return new();
            }
        }

        public async Task<Test> UpdateAsync(long Id,Test test)
        {
            try
            {
                var oldtest=await _dbContext.Tests.FirstOrDefaultAsync(t => t.Id == Id);
                oldtest!.TestStatus = test.TestStatus;
                oldtest!.TestCode = test.TestCode;
                oldtest!.Name = test.Name;
                oldtest!.Describtion = test.Describtion;
                oldtest!.AnswerKey = test.AnswerKey;
                oldtest!.UpdateAt = DateTime.UtcNow.AddHours(-5);

                _dbContext.Tests.Update(oldtest);
                await _dbContext.SaveChangesAsync();
                return test;
            }
            catch
            {
                return new();
            }
        }
    }
}
