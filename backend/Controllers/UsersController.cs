using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using backend.Data;
using backend.DTOs;
using backend.Models;
using Castle.Core.Internal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    // [ServiceFilter(typeof(LogUserActivity))]
    [Authorize(Roles = "Fejlesztő")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IRepository _repo;
        private readonly UserManager<User> _userManager;

        public UsersController(IRepository repo, IMapper mapper, ApplicationDbContext context,
            UserManager<User> userManager)
        {
            _repo = repo;
            _mapper = mapper;
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _repo.GetUsers();
            var usersToReturn = _mapper.Map<IEnumerable<UserForListDto>>(users);

            return Ok(usersToReturn);
        }

        [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repo.GetUser(id);
            var userToReturn = _mapper.Map<UserForListDto>(user);

            return Ok(userToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserForUpdateDto userForUpdateDto)
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)) return Unauthorized();

            try
            {
                var userFromRepo = await _repo.GetUser(id);
                _mapper.Map(userForUpdateDto, userFromRepo);

                if (await _repo.SaveAll()) return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

            return BadRequest();
            // throw new Exception($"Failed to save user with ID {id}");
        }

        [HttpGet("{id}/accounts")]
        public async Task<IActionResult> GetAccounts(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            return Ok(await _context.CrAccounts.Where(a => a.User == user).ToListAsync());
        }

        [Authorize(Roles = "Fejlesztő")]
        [HttpGet("accounts")]
        public async Task<IActionResult> GetAccounts()
        {
            return Ok(await _context.CrAccounts.Select(a => new {id = a.Id, userId = a.User.Id, a.User.UserName, a.PlayerId, a.IsVerified}).ToListAsync());
        }

        [HttpPost("{id}/accounts/{playerId}")]
        public async Task<IActionResult> AddCrAccount(int id, string playerId)
        {
            try
            {
                if (!Regex.Match(playerId, @"[0289CcGgJjLlPpQqRrUuVvYy]*").Success)
                {
                    return BadRequest();
                }

                var account =
                    await _context.CrAccounts.FirstOrDefaultAsync(a =>
                        a.PlayerId == playerId && a.IsVerified);

                if (account != null) return BadRequest(new {exists = true});

                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
                var crAccount = new CrAccount
                {
                    PlayerId = playerId,
                    IsVerified = false,
                    User = user
                };

                await _context.CrAccounts.AddAsync(crAccount);
                await _context.SaveChangesAsync();

                return Ok(await _context.CrAccounts.FirstOrDefaultAsync(a =>
                    a.PlayerId == playerId && a.User == user));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpDelete("{id}/accounts/{playerId}")]
        public async Task<IActionResult> DeleteCrAccount(int id, string playerId)
        {
            try
            {
                var crAccount = await _context.CrAccounts.FirstOrDefaultAsync(a => a.PlayerId == playerId && a.User.Id == id);
                var user = await _userManager.GetUserAsync(User);

                if (!await _userManager.IsInRoleAsync(user, "Fejlesztő") && crAccount.User.Id != user.Id) return Forbid();

                _context.Remove(crAccount);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // [AllowAnonymous]
        [HttpPost("{id}/accounts/{playerId}/verify")]
        public async Task<IActionResult> VerifyCrAccount(int id, string playerId)
        {
            try
            {
                var crAccount = await _context.CrAccounts.FirstOrDefaultAsync(a => a.PlayerId == playerId && a.User.Id == id);
                var user = await _userManager.GetUserAsync(User);

                if (!await _userManager.IsInRoleAsync(user, "Fejlesztő") && crAccount.User.Id != user.Id) return Forbid();

                var relatedCrAccounts = await _context.CrAccounts.Where(a => a.PlayerId == playerId && a.IsVerified).ToListAsync();
                
                if (!relatedCrAccounts.IsNullOrEmpty())
                {
                    return BadRequest(new {message = "Ezt a fiókot már megerősítették."});
                }
                
                crAccount.IsVerified = true;
                await _context.SaveChangesAsync();
                
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            var currentUser = await _userManager.GetUserAsync(User);

            if (user.Id == currentUser.Id)
            {
                return BadRequest();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("{id}/roles")]
        public async Task<IActionResult> GetUserRoles(int id)
        {
            return Ok(await _userManager.GetRolesAsync(await _context.Users.FirstOrDefaultAsync(u => u.Id == id)));
        }
    }
}