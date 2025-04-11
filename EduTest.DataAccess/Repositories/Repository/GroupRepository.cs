using EduTest.DataAccess.DBContext;
using EduTest.DataAccess.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EduTest.DataAccess.Repositories.Repository
{
    public class GroupRepository : IGroupRepository
    {
        private readonly AppDbContext _dbContext;
        public GroupRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Domain.Models.Group> AddAsync(Domain.Models.Group group)
        {
           try
            {
                await _dbContext.Groups.AddAsync(group);
                await _dbContext.SaveChangesAsync();
                return group;
            }
            catch
            {
                return new();
            }
        }

        public async Task<bool> DeleteAsync(long id)
        {
            try
            {
                var group = await _dbContext.Groups.FirstOrDefaultAsync(a => a.Id == id);
                _dbContext.Groups.Remove(group!);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Domain.Models.Group>> GetAllAsync()
        {
            try
            {
                return await _dbContext.Groups
                    .Include(g => g.Teacher)
                    .Include(g => g.Students)
                    .ToListAsync();
            }
            catch
            {
                return new List<Domain.Models.Group>();
            }
        }

        public async Task<Domain.Models.Group> GetByIdAsync(long id)
        {
            try
            {
                var group= await _dbContext.Groups
                       .Include(g => g.Teacher)
                       .Include(g => g.Students)
                       .FirstOrDefaultAsync(g => g.Id == id);
                return group!;
            }
            catch
            {
                return new();
            }
        }

        public async Task<Domain.Models.Group> UpdateAsync(long Id, Domain.Models.Group group)
        {
            try
            {
               var oldgroup= await _dbContext.Groups.FirstOrDefaultAsync(g => g.Id == Id);
               
                oldgroup!.Name=group.Name;
                oldgroup!.TeacherId=group.TeacherId;
                oldgroup!.Describtion=group.Describtion;
                oldgroup!.UpdateAt = DateTime.UtcNow.AddHours(-5);


                _dbContext.Groups.Update(oldgroup);
                await _dbContext.SaveChangesAsync();
                return group;
            }
            catch
            {
                return new();
            }
        }
    }
}
