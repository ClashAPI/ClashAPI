using System;
using System.Threading.Tasks;
using backend.Models;

namespace backend.Repositories
{
    public interface IFollowRepository
    {
        Task AddPlayerFollowAsync<T>(T entity, Guid userId) where T : PlayerFollow;
        void RemovePlayerFollow(PlayerFollow playerFollow);
        Task AddClanFollowAsync<T>(T entity, Guid userId) where T : ClanFollow;
        void RemoveClanFollow(ClanFollow clanFollow);
        Task<bool> SaveAllAsync();
    }
}