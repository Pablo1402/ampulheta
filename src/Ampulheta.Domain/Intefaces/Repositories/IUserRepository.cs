using Ampulheta.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ampulheta.Domain.Intefaces.Repositories
{

    public interface IUserRepository
    {
        Task<User> GetByLogin(string login);
        Task<User> GetById(int id);
        Task<List<User>> GetPaged(int pageIndex, int pageSize);
        Task SaveAsync(User user);
        Task UpdateAsync(User user);
    }
}
