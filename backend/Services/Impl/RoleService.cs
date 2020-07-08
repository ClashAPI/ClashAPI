using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Models;
using backend.Repositories;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace backend.Services.Impl
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<EntityEntry<T>> AddAsync<T>(T entity) where T : Role
        {
            return await _roleRepository.AddAsync(entity);
        }

        public void Remove<T>(T entity) where T : Role
        {
            _roleRepository.Remove(entity);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _roleRepository.SaveAllAsync();
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await _roleRepository.GetAllAsync();
        }

        public async Task<Role> GetByIdAsync(Guid id)
        {
            return await _roleRepository.GetByIdAsync(id);
        }

        public async Task<Role> GetByNameAsync(string name)
        {
            return await _roleRepository.GetByNameAsync(name);
        }
    }
}