using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace backend.Repositories.Impl
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<EntityEntry<T>> AddAsync<T>(T entity) where T : User
        {
            return await _context.AddAsync(entity);
        }

        public void Remove<T>(T entity) where T : User
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
        }
    }
}