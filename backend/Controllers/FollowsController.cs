using System;
using System.Linq;
using System.Threading.Tasks;
using backend.DTOs;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FollowsController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IFollowService _followService;

        public FollowsController(UserManager<User> userManager, IFollowService followService)
        {
            _userManager = userManager;
            _followService = followService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (!user.PlayerFollows.ToList().Any() && !user.ClanFollows.ToList().Any())
            {
                return Ok(new {isFollowingAnyone = false});
            }

            var playerFollows = _followService.GetPlayerFollowsByUser(user);
            var clanFollows = _followService.GetClanFollowsByUser(user);

            return Ok(new {playerFollows, clanFollows});
        }

        [HttpPost("remove/player")]
        public async Task<IActionResult> UnfollowPlayer(PlayerUnfollowDto playerUnfollowDto)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var follow = await _followService.GetPlayerFollowByPlayerTagAndUserAsync(playerUnfollowDto.Id, user);
                
                if (follow == null)
                {
                    return BadRequest();
                }

                _followService.RemovePlayerFollow(follow);
                await _followService.SaveAllAsync();
            }
            catch (Exception e)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPost("remove/clan")]
        public async Task<IActionResult> UnfollowClan(ClanUnfollowDto clanUnfollowDto)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);

                if (user.ClanFollows.FirstOrDefault(f => f.ClanTag == clanUnfollowDto.Id) == null)
                {
                    return BadRequest();
                }

                var follow = user.ClanFollows.FirstOrDefault(f => f.ClanTag == clanUnfollowDto.Id);
                _followService.RemoveClanFollow(follow);
                await _followService.SaveAllAsync();
            }
            catch (Exception e)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPost("new/player")]
        public async Task<IActionResult> FollowPlayer(PlayerFollowDto playerFollowDto)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);

                if (user.PlayerFollows.FirstOrDefault(f => f.PlayerTag == playerFollowDto.Id) != null)
                {
                    return BadRequest();
                }
                
                await _followService.AddPlayerFollowAsync(new PlayerFollow
                {
                    PlayerTag = playerFollowDto.Id,
                    PlayerName = playerFollowDto.PlayerName,
                    Follower = user,
                }, user.Id);
                await _followService.SaveAllAsync();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

            return Ok();
        }

        [HttpPost("new/clan")]
        public async Task<IActionResult> FollowClan(ClanFollowDto clanFollowDto)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);

                if (user.ClanFollows.FirstOrDefault(f => f.ClanTag == clanFollowDto.Id) != null)
                {
                    return BadRequest();
                }
                
                await _followService.AddClanFollowAsync(new ClanFollow
                {
                    ClanName = clanFollowDto.ClanName,
                    ClanTag = clanFollowDto.Id,
                    Follower = user,
                }, user.Id);
                await _followService.SaveAllAsync();
            }
            catch (Exception e)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}