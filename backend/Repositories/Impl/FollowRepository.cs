using System;
using System.Threading.Tasks;
using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories.Impl
{
    public class FollowRepository : IFollowRepository
    {
        private readonly ApplicationDbContext _context;

        public FollowRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddPlayerFollowAsync<T>(T entity, Guid userId) where T : PlayerFollow
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            user.PlayerFollows.Add(entity);
        }

        public void RemovePlayerFollow(PlayerFollow playerFollow)
        {
            _context.Remove(playerFollow);
        }
        
        public async Task AddClanFollowAsync<T>(T entity, Guid userId) where T : ClanFollow
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            user.ClanFollows.Add(entity);
        }

        public void RemoveClanFollow(ClanFollow playerFollow)
        {
            _context.Remove(playerFollow);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}