using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Pekka.ClashRoyaleApi.Client.Models.PlayerModels;

namespace backend.Data
{
    public interface IRepository
    {
        Task<EntityEntry<T>> AddAsync<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAllAsync();
        Task<IEnumerable<User>> GetUsersAsync();
        Task<IEnumerable<User>> GetUsersRegisteredTodayAsync();
        Task<User> GetUserAsync(Guid id);
        Task<User> GetUserAsync(string userName);
        Task<List<string>> GetBadgesAsync(string playerTag);
        Task<bool> GetIsFollowingPlayerAsync(string id, System.Security.Claims.ClaimsPrincipal user);
        Task<bool> GetIsFollowingClanAsync(string id, System.Security.Claims.ClaimsPrincipal user);
        Task<CrAccount> GetCrAccountAsync(Guid userId, string playerId);
        Task<List<CrAccount>> GetCrAccountsAsync(string playerId);
        Task<bool> GetIfCrAccountExistsAsync(string playerId);
        Task<bool> GetIfAnnouncementExistsAsync(Guid announcementId);
        Task<Announcement> GetAnnouncementAsync(Guid announcementId);
    }
}