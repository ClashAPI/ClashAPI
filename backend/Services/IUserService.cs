using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace backend.Services
{
    public interface IUserService
    {
        Task<EntityEntry<T>> AddAsync<T>(T entity) where T : User;
        void Remove<T>(T entity) where T : User;
        Task<bool> SaveAllAsync();
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(Guid id);
        Task<User> GetByUsernameAsync(string username);
        Task<IEnumerable<string>> GetBadgesByPlayerTagAsync(string playerTag);
        Task<IEnumerable<User>> GetAllRegisteredTodayAsync();
    }
}