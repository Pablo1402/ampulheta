using Ampulheta.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ampulheta.Domain.Intefaces.Repositories
{
    public interface IProjectRepository
    {
        Task SaveAsync(Project project);
        Task UpdateAsync(Project project);
        Task<List<Project>> GetPaged(int pageIndex, int pageSize);
        Task<Project> GetById(int id);
    }
}
