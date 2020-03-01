using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using backend.Data;
using backend.DTOs;
using backend.Models;
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
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public FollowsController(UserManager<User> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (!user.PlayerFollows.ToList().Any() && !user.ClanFollows.ToList().Any())
                return Ok(new {isFollowingAnyone = false});

            var playerFollows = user.PlayerFollows.Select(p => new {p.PlayerTag, p.PlayerName}).ToList();
            var clanFollows = user.ClanFollows.Select(c => new {c.ClanTag, c.ClanName}).ToList();

            return Ok(new {playerFollows, clanFollows});
        }

        [HttpPost("remove/player")]
        public async Task<IActionResult> UnfollowPlayer(PlayerUnfollowDto playerUnfollowDto)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);

                if (user.PlayerFollows.FirstOrDefault(f => f.PlayerTag == playerUnfollowDto.Id) == null)
                    return BadRequest();

                var follow = user.PlayerFollows.FirstOrDefault(f => f.PlayerTag == playerUnfollowDto.Id);
                _context.Remove(follow);
                await _context.SaveChangesAsync();
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

                if (user.ClanFollows.FirstOrDefault(f => f.ClanTag == clanUnfollowDto.Id) == null) return BadRequest();

                var follow = user.ClanFollows.FirstOrDefault(f => f.ClanTag == clanUnfollowDto.Id);
                _context.Remove(follow);
                await _context.SaveChangesAsync();
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
                    return BadRequest();

                user.PlayerFollows.Add(new PlayerFollow
                    {PlayerTag = playerFollowDto.Id, PlayerName = playerFollowDto.PlayerName, Follower = user});
                await _context.SaveChangesAsync();
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

                if (user.ClanFollows.FirstOrDefault(f => f.ClanTag == clanFollowDto.Id) != null) return BadRequest();

                user.ClanFollows.Add(new ClanFollow
                    {ClanName = clanFollowDto.ClanName, ClanTag = clanFollowDto.Id, Follower = user});
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}