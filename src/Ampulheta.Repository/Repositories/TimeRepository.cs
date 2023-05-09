using Ampulheta.Domain.Entities;
using Ampulheta.Domain.Intefaces.Repositories;
using Ampulheta.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace Ampulheta.Repository.Repositories
{
    public class TimeRepository : ITimeRepository
    {
        private readonly AmpulhetaContext _context;

        public TimeRepository(AmpulhetaContext context)
        {
            _context = context;
        }

        public async Task<Time> GetById(int id)
        {
            return await _context.Times
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Time>> GetByProjetc(int projetcId)
        {
            return await _context.Times
               .Where(t => t.ProjectId == projetcId)
               .AsNoTracking()
               .Include(x => x.User)
               .ToListAsync();
        }

        public async Task<List<Time>> GetByUser(int userId)
        {
            return await _context.Times
                .Where(t => t.UserId == userId)
                .AsNoTracking()
                .Include(x => x.Project)
                .ToListAsync();
        }

        public async Task SaveAsync(Time time)
        {
            await _context.Times.AddAsync(time);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Time time)
        {
            _context.Times.Update(time);
            await _context.SaveChangesAsync();
        }
    }
}
