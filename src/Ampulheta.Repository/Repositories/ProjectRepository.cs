using Ampulheta.Domain.Entities;
using Ampulheta.Domain.Intefaces.Repositories;
using Ampulheta.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ampulheta.Repository.Repositories
{

    public class ProjectRepository : IProjectRepository
    {
        private readonly AmpulhetaContext _context;

        public ProjectRepository(AmpulhetaContext context)
        {
            _context = context;
        }

        public async Task<Project> GetById(int id)
        {
            return await _context.Projects
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Project>> GetPaged(int pageIndex, int pageSize)
        {
            var index = 0;
            if (pageIndex > 1)
                index = (pageIndex - 1) * pageSize;

            return await _context.Projects
                .AsNoTracking()
                .Skip(index)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task SaveAsync(Project project)
        {
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Project project)
        {
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
        }
    }
}
