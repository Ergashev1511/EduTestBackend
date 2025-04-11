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
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _dbContext;
        public StudentRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Student> AddAsync(Student student)
        {
            try
            {
                await _dbContext.Students.AddAsync(student);
                await _dbContext.SaveChangesAsync();
                return student;
            }
            catch
            {
                return new Student();
            }
        }

        public async Task<bool> DeleteAsync(long Id)
        {
            try
            {
                var student = await _dbContext.Students.FirstOrDefaultAsync(a => a.Id ==Id);

                _dbContext.Students.Remove(student!);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Student>> GetAllAsync()
        {
            try
            {
                return await _dbContext.Students
                      .Include(s => s.Teacher)
                      .Include(s => s.Group)
                      .Include(s => s.Tests)
                      .Include(s => s.TestResults)
                          .ThenInclude(tr => tr.Test)
                      .ToListAsync();
            }
            catch
            {
                return new List<Student>();
            }
        }

        public async Task<Student> GetByIdAsync(long Id)
        {
            try
            {
                var student= await _dbContext.Students
                      .Include(s => s.Teacher)
                      .Include(s => s.Group)
                      .Include(s => s.Tests)
                      .Include(s => s.TestResults)
                          .ThenInclude(tr => tr.Test)
                      .FirstOrDefaultAsync(s => s.Id == Id);
                return student!;
            }
            catch
            {
                return new Student();
            }
        }

        public async Task<Student> GetByUserNameAsync(string userName)
        {
            try
            {
                var student = await _dbContext.Students
                      .Include(s => s.Teacher)
                      .Include(s => s.Group)
                      .Include(s => s.Tests)
                      .Include(s => s.TestResults)
                          .ThenInclude(tr => tr.Test)
                      .FirstOrDefaultAsync(s => s.UserName==userName);
                return student!;
            }
            catch
            {
                return new Student();
            }
        }

        public async Task<Student> UpdateAsync(long Id, Student student)
        {
            try
            {
                var students = await _dbContext.Students.FirstOrDefaultAsync(s => s.Id == Id);
                students!.FirstName = student.FirstName;
                students!.LastName = student.LastName;
                students!.PhoneNumber = student.PhoneNumber;
                students!.GroupId = student.GroupId;
                students!.TeacherId = student.TeacherId;
                students!.UserName = student.UserName;
                students!.PasswordHash = student.PasswordHash;
                students!.PasswordSalt = student.PasswordSalt;
                student!.UpdateAt= DateTime.UtcNow.AddHours(-5);

                _dbContext.Students.Update(students);
                await _dbContext.SaveChangesAsync();

                return student;
            }
            catch
            {
                return new Student();
            }
        }
    }
}
