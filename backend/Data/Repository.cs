using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace backend.Data
{
    public class Repository : IRepository
    {
        private readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<EntityEntry<T>> AddAsync<T>(T entity) where T : class
        {
            return await _context.AddAsync(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<IEnumerable<User>> GetUsersRegisteredTodayAsync()
        {
            return await _context.Users.Where(u => u.CreatedAt.Date == DateTime.Today).ToListAsync();
        }

        public async Task<User> GetUserAsync(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetUserAsync(string userName)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        }

        public async Task<List<string>> GetBadgesAsync(string playerTag)
        {
            var badges = new List<string>();
            var account = await _context.CrAccounts.FirstOrDefaultAsync(a => a.PlayerId == playerTag && a.IsVerified);

            if (account == null)
                return null;

            badges.Add("verified");

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == account.User.Id);
            var devRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "Fejlesztő");

            if (user.UserRoles.FirstOrDefault(r => r.Role == devRole) != null) badges.Add("staff");

            return badges;
        }

        public async Task<bool> GetIsFollowingPlayerAsync(string playerTag, System.Security.Claims.ClaimsPrincipal user)
        {
            var isFollowing = false;

            if (user.Identity.IsAuthenticated)
            {
                var userFromRepo = await _context.Users.FirstOrDefaultAsync(u => u.UserName == user.Identity.Name);
                isFollowing = userFromRepo.PlayerFollows.FirstOrDefault(f => f.PlayerTag == playerTag) != null;
            }

            return isFollowing;
        }

        public async Task<bool> GetIsFollowingClanAsync(string clanTag, ClaimsPrincipal user)
        {
            var isFollowing = false;

            if (user.Identity.IsAuthenticated)
            {
                var userFromRepo = await _context.Users.FirstOrDefaultAsync(u => u.UserName == user.Identity.Name);
                isFollowing = userFromRepo.ClanFollows.FirstOrDefault(f => f.ClanTag == clanTag) != null;
            }

            return isFollowing;
        }
        
        public async Task<CrAccount> GetCrAccountAsync(Guid userId, string playerId)
        {
            return await _context.CrAccounts.FirstOrDefaultAsync(a => a.PlayerId == playerId && a.User.Id == userId);
        }

        public async Task<List<CrAccount>> GetCrAccountsAsync(string playerId)
        {
            return await _context.CrAccounts.Where(a => a.PlayerId == playerId && a.IsVerified).ToListAsync();
        }

        public async Task<bool> GetIfCrAccountExistsAsync(string playerId)
        {
            return await _context.CrAccounts.FirstOrDefaultAsync(a =>
                a.PlayerId == playerId && a.IsVerified) != null;
        }

        public async Task<bool> GetIfAnnouncementExistsAsync(Guid announcementId)
        {
            return _context.Announcements.Any(e => e.Id == announcementId);
        }

        public async Task<Announcement> GetAnnouncementAsync(Guid announcementId)
        {
            return await _context.Announcements.FirstOrDefaultAsync(a => a.Id == announcementId);
        }
    }
}