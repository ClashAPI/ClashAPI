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
        private readonly IRepository _repository;
        private readonly UserManager<User> _userManager;

        public UsersController(IRepository repository, IMapper mapper, ApplicationDbContext context,
            UserManager<User> userManager)
        {
            _repository = repository;
            _mapper = mapper;
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _repository.GetUsersAsync();
            var usersToReturn = _mapper.Map<IEnumerable<UserForListDto>>(users);

            return Ok(usersToReturn);
        }

        [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repository.GetUserAsync(id);
            var userToReturn = _mapper.Map<UserForListDto>(user);

            return Ok(userToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserForUpdateDto userForUpdateDto)
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)) return Unauthorized();

            try
            {
                var userFromRepo = await _repository.GetUserAsync(id);
                _mapper.Map(userForUpdateDto, userFromRepo);

                if (await _repository.SaveAllAsync()) return NoContent();
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
            var user = await _repository.GetUserAsync(id);

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

                if (await _repository.GetIfCrAccountExistsAsync(playerId)) return BadRequest(new {exists = true});

                var user = await _repository.GetUserAsync(id);
                var crAccount = new CrAccount
                {
                    PlayerId = playerId,
                    IsVerified = false,
                    User = user
                };

                _repository.AddAsync(crAccount);
                await _repository.SaveAllAsync();

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
                var crAccount = await _repository.GetCrAccountAsync(id, playerId);
                var user = await _userManager.GetUserAsync(User);

                if (!await _userManager.IsInRoleAsync(user, "Fejlesztő") && crAccount.User.Id != user.Id) return Forbid();

                _repository.Delete(crAccount);
                await _repository.SaveAllAsync();

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
                var crAccount = await _repository.GetCrAccountAsync(id, playerId);
                var user = await _userManager.GetUserAsync(User);

                if (!await _userManager.IsInRoleAsync(user, "Fejlesztő") && crAccount.User.Id != user.Id) return Forbid();

                var relatedCrAccounts = await _repository.GetCrAccountsAsync(playerId);
                
                if (!relatedCrAccounts.IsNullOrEmpty())
                {
                    return BadRequest(new {message = "Ezt a fiókot már megerősítették."});
                }
                
                crAccount.IsVerified = true;
                await _repository.SaveAllAsync();
                
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
            var user = await _repository.GetUserAsync(id);
            var currentUser = await _userManager.GetUserAsync(User);

            if (user.Id == currentUser.Id)
            {
                return BadRequest();
            }

            _repository.Delete(user);
            await _repository.SaveAllAsync();

            return Ok();
        }

        [HttpGet("{id}/roles")]
        public async Task<IActionResult> GetUserRoles(int id)
        {
            return Ok(await _userManager.GetRolesAsync(await _repository.GetUserAsync(id)));
        }
    }
}