using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using backend.Models;
using backend.Repositories;

namespace backend.Services.Impl
{
    public class FollowService : IFollowService
    {
        private readonly IUserService _userService;
        private readonly IFollowRepository _followRepository;

        public FollowService(IFollowRepository followRepository, IUserService userService)
        {
            _followRepository = followRepository;
            _userService = userService;
        }

        public async Task AddPlayerFollowAsync<T>(T entity, Guid userId) where T : PlayerFollow
        {
            await _followRepository.AddPlayerFollowAsync(entity, userId);
        }
        public void RemovePlayerFollow(PlayerFollow playerFollow)
        {
            _followRepository.RemovePlayerFollow(playerFollow);
        }

        public async Task AddClanFollowAsync<T>(T entity, Guid userId) where T : ClanFollow
        {
            await _followRepository.AddClanFollowAsync(entity, userId);
        }

        public void RemoveClanFollow(ClanFollow clanFollow)
        {
            _followRepository.RemoveClanFollow(clanFollow);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _followRepository.SaveAllAsync();
        }
        
        public async Task<bool> GetIsFollowingPlayerByPlayerTagAndUserAsync(string id, ClaimsPrincipal user)
        {
            var isFollowing = false;

            if (user.Identity.IsAuthenticated)
            {
                var dbUser = await _userService.GetByUsernameAsync(user.Identity.Name);
                isFollowing = dbUser.PlayerFollows
                    .FirstOrDefault(f => f.PlayerTag == id) != null;
            }

            return isFollowing;
        }

        public async Task<bool> GetIsFollowingClanByClanTagAndUserAsync(string id, ClaimsPrincipal user)
        {
            var isFollowing = false;

            if (user.Identity.IsAuthenticated)
            {
                var dbUser = await _userService.GetByUsernameAsync(user.Identity.Name);
                isFollowing = dbUser.ClanFollows.
                    FirstOrDefault(f => f.ClanTag == id) != null;
            }

            return isFollowing;
        }

        public async Task<PlayerFollow> GetPlayerFollowByPlayerTagAndUserAsync(string playerTag, User user)
        {
            var dbUser = await _userService.GetByUsernameAsync(user.UserName);
            return dbUser.PlayerFollows
                .FirstOrDefault(f => f.PlayerTag == playerTag);
        }
        
        public async Task<ClanFollow> GetClanFollowByClanTagAndUserAsync(string clanTag, User user)
        {
            var dbUser = await _userService.GetByUsernameAsync(user.UserName);
            return dbUser.ClanFollows
                .FirstOrDefault(f => f.ClanTag == clanTag);
        }

        public Object GetPlayerFollowsByUser(User user)
        {
            return user.PlayerFollows.Select(p => new {p.PlayerTag, p.PlayerName}).ToList();
        }
        
        public Object GetClanFollowsByUser(User user)
        {
            return user.ClanFollows.Select(c => new {c.ClanTag, c.ClanName}).ToList();
        }
    }
}