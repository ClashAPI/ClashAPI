using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace backend.Repositories
{
    public interface IRoleRepository
    {
        Task<EntityEntry<T>> AddAsync<T>(T entity) where T : Role;
        void Remove<T>(T entity) where T : Role;
        Task<bool> SaveAllAsync();
        Task<IEnumerable<Role>> GetAllAsync();
        Task<Role> GetByIdAsync(Guid id);
        Task<Role> GetByNameAsync(string name);
    }
}