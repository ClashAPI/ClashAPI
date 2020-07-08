using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace backend.Repositories.Impl
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<EntityEntry<T>> AddAsync<T>(T entity) where T : Role
        {
            return await _context.AddAsync(entity);
        }

        public void Remove<T>(T entity) where T : Role
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role> GetByIdAsync(Guid id)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Role> GetByNameAsync(string name)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.Name == name);
        }
    }
}