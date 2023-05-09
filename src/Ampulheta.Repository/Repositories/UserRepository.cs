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
    public class UserRepository : IUserRepository
    {
        private readonly AmpulhetaContext _context;

        public UserRepository(AmpulhetaContext context)
        {
            _context = context;
        }

        public async Task<User> GetById(int id)
        {
            return await _context.Users
                 .AsNoTracking()
                 .Include(x => x.UserType)
                 .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> GetByLogin(string login)
        {
            return await _context.Users
                .AsNoTracking()
                .Include(x => x.UserType)
                .FirstOrDefaultAsync(x => x.Login == login);
        }

        public async Task<List<User>> GetPaged(int pageIndex, int pageSize)
        {
            var index = 0;
            if(pageIndex > 1)
                index = (pageIndex - 1)* pageSize;

            return await _context.Users
                .AsNoTracking()
                .Skip(index)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task SaveAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
