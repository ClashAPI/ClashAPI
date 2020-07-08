using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;
using backend.Repositories;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace backend.Services.Impl
{
    public class CRAccountService : ICRAccountService
    {
        private readonly ICRAccountRepository _crAccountRepository;

        public CRAccountService(ICRAccountRepository crAccountRepository)
        {
            _crAccountRepository = crAccountRepository;
        }

        public async Task<EntityEntry<T>> AddAsync<T>(T entity) where T : CrAccount
        {
            return await _crAccountRepository.AddAsync(entity);
        }

        public void Remove<T>(T entity) where T : CrAccount
        {
            _crAccountRepository.Remove(entity);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _crAccountRepository.SaveAllAsync();
        }

        public async Task<IEnumerable<CrAccount>> GetAllAsync()
        {
            return await _crAccountRepository.GetAllAsync();
        }

        public async Task<CrAccount> GetByIdAsync(Guid id)
        {
            return await _crAccountRepository.GetByIdAsync(id);
        }

        public async Task<CrAccount> GetByPlayerTagAsync(string playerTag, bool isVerified = true)
        {
            return await _crAccountRepository.GetByPlayerTagAsync(playerTag);
        }

        public async Task<CrAccount> GetByUserIdAndPlayerIdAsync(Guid userId, string playerId)
        {
            return await _crAccountRepository.GetByUserIdAndPlayerIdAsync(userId, playerId);
        }

        public async Task<bool> GetIfExistsByPlayerTagAsync(string playerId)
        {
            var result = await _crAccountRepository.GetByPlayerTagAsync(playerId);
            return result != null;
        }

        public async Task<IEnumerable<CrAccount>> GetByUserIdAsync(Guid userId)
        {
            var accounts = await _crAccountRepository.GetAllAsync();
            return accounts.Where(a => a.User.Id == userId).ToList();
        }
    }
}