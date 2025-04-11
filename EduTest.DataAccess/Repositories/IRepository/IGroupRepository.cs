using EduTest.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTest.DataAccess.Repositories.IRepository
{
    public interface IGroupRepository
    {
        Task<Group>  AddAsync(Group group);
        Task<Group> UpdateAsync(long Id,Group group);
        Task<bool> DeleteAsync(long id);
        Task<Group> GetByIdAsync(long id);
        Task<List<Group>> GetAllAsync();

    }
}
