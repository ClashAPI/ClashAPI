using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace backend.Repositories.Impl
{
    public class CRAccountRepository : ICRAccountRepository
    {
        private readonly ApplicationDbContext _context;

        public CRAccountRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<EntityEntry<T>> AddAsync<T>(T entity) where T : CrAccount
        {
            return await _context.AddAsync(entity);
        }

        public void Remove<T>(T entity) where T : CrAccount
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<CrAccount>> GetAllAsync()
        {
            return await _context.CrAccounts.ToListAsync();
        }

        public async Task<CrAccount> GetByIdAsync(Guid id)
        {
            return await _context.CrAccounts.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<CrAccount> GetByPlayerTagAsync(string playerTag, bool isVerified = true)
        {
            return await _context.CrAccounts.FirstOrDefaultAsync(a => a.PlayerId == playerTag && a.IsVerified == isVerified);
        }

        public async Task<CrAccount> GetByUserIdAndPlayerIdAsync(Guid userId, string playerId)
        {
            return await _context.CrAccounts.FirstOrDefaultAsync(a => a.PlayerId == playerId && a.User.Id == userId);
        }
    }
}