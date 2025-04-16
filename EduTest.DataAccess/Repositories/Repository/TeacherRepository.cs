using EduTest.DataAccess.DBContext;
using EduTest.DataAccess.Repositories.IRepository;
using EduTest.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTest.DataAccess.Repositories.Repository
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly AppDbContext _dbContext;
        public TeacherRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Teacher> AddAsync(Teacher teacher)
        {
            try
            {
                await _dbContext.Teachers.AddAsync(teacher);
                await _dbContext.SaveChangesAsync();
                return teacher;
            }
            catch
            {
                return new Teacher();
            }
        }

        public async Task<bool> DeleteAsync(long Id)
        {
           try
            {
                var teacher = await _dbContext.Teachers.FirstOrDefaultAsync(x => x.Id == Id);

                _dbContext.Teachers.Remove(teacher!);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Teacher>> GetAllAsync()
        {
            try
            {
                return await _dbContext.Teachers
                              .Include(t => t.Students)
                         .Include(t => t.Groups)
                             .ThenInclude(g => g.Students)
                         .Include(t => t.Tests)
                             .ThenInclude(test => test.TestResults)
                         .ToListAsync();
            }
            catch
            {
                return new List<Teacher>();
            }
        }

        public async Task<Teacher> GetByIdAsnc(long Id)
        {
            try
            {
                var teacher= await _dbContext.Teachers
                        .Include(t => t.Students)
                    .Include(t => t.Groups)
                        .ThenInclude(g => g.Students)
                    .Include(t => t.Tests)
                        .ThenInclude(test => test.TestResults)
                            .ThenInclude(result => result.Student)
                    .Include(t => t.Tests)
                        .ThenInclude(test => test.TestResults)
                            .ThenInclude(result => result.Test)
                    .FirstOrDefaultAsync(t => t.Id == Id!);

                return teacher!;
            }
            catch
            {
                return new Teacher();
            }
       
        }

        public async Task<Teacher> GetByUserNameAsync(string userName)
        {
            try
            {
                var teacher = await _dbContext.Teachers
                    .FirstOrDefaultAsync(t => t.UserName==userName);

                return teacher!;
            }
            catch
            {
                return new Teacher();
            }

        }

        public async Task<Teacher> UpdateAsync(long Id,Teacher teacher)
        {
            try
            {
                var oldteacher=await _dbContext.Teachers.FirstOrDefaultAsync(t => t.Id == Id);
                oldteacher!.FirstName = teacher.FirstName;
                oldteacher!.LastName = teacher.LastName;
                oldteacher!.UserName = teacher.UserName;
                oldteacher!.PasswordSalt = teacher.PasswordSalt;
                oldteacher!.PasswordHash = teacher.PasswordHash;
                oldteacher!.UpdateAt = DateTime.UtcNow.AddHours(-5);


                 _dbContext.Teachers.Update(teacher);
                await _dbContext.SaveChangesAsync();
                return teacher;
            }
            catch
            {
                return new Teacher();
            }
        }
    }
}
