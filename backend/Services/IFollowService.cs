using System;
using System.Security.Claims;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace backend.Services
{
    public interface IFollowService
    {
        Task AddPlayerFollowAsync<T>(T entity, Guid userId) where T : PlayerFollow;
        void RemovePlayerFollow(PlayerFollow playerFollow);
        Task AddClanFollowAsync<T>(T entity, Guid userId) where T : ClanFollow;
        void RemoveClanFollow(ClanFollow clanFollow);
        Task<bool> SaveAllAsync();
        Task<bool> GetIsFollowingPlayerByPlayerTagAndUserAsync(string id, ClaimsPrincipal user);
        Task<bool> GetIsFollowingClanByClanTagAndUserAsync(string id, ClaimsPrincipal user);
        Task<PlayerFollow> GetPlayerFollowByPlayerTagAndUserAsync(string playerTag, User user);
        Task<ClanFollow> GetClanFollowByClanTagAndUserAsync(string clanTag, User user);
        Object GetPlayerFollowsByUser(User user);
        Object GetClanFollowsByUser(User user);
    }
}