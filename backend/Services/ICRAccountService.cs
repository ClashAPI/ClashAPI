using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace backend.Services
{
    public interface ICRAccountService
    {
        Task<EntityEntry<T>> AddAsync<T>(T entity) where T : CrAccount;
        void Remove<T>(T entity) where T : CrAccount;
        Task<bool> SaveAllAsync();
        Task<IEnumerable<CrAccount>> GetAllAsync();
        Task<CrAccount> GetByIdAsync(Guid id);
        Task<CrAccount> GetByPlayerTagAsync(string playerTag, bool isVerified = true);
        Task<CrAccount> GetByUserIdAndPlayerIdAsync(Guid userId, string playerId);
        Task<bool> GetIfExistsByPlayerTagAsync(string playerId);
        Task<IEnumerable<CrAccount>> GetByUserIdAsync(Guid userId);
    }
}