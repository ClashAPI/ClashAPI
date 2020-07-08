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
using backend.Repositories;
using backend.Services;
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
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;
        private readonly ICRAccountService _crAccountService;

        public UsersController(IMapper mapper, UserManager<User> userManager, 
            IUserService userService, ICRAccountService crAccountService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _userService = userService;
            _crAccountService = crAccountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetAllAsync();
            var usersToReturn = _mapper.Map<IEnumerable<UserForListDto>>(users);

            return Ok(usersToReturn);
        }

        [AllowAnonymous]
        [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            var userToReturn = _mapper.Map<UserForListDto>(user);

            return Ok(userToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, UserForUpdateDto userForUpdateDto)
        {
            if (id != Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)) return Unauthorized();

            try
            {
                var userFromRepo = await _userService.GetByIdAsync(id);
                _mapper.Map(userForUpdateDto, userFromRepo);

                if (await _userService.SaveAllAsync())
                {
                    return NoContent();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

            return BadRequest();
            // throw new Exception($"Failed to save user with ID {id}");
        }

        [AllowAnonymous]
        [HttpGet("{id}/accounts")]
        public async Task<IActionResult> GetAccounts(Guid id)
        {
            return Ok(await _crAccountService.GetByUserIdAsync(id));
        }

        [Authorize(Roles = "Fejlesztő")]
        [HttpGet("accounts")]
        public async Task<IActionResult> GetAccounts()
        {
            return Ok(await _crAccountService.GetAllAsync());
        }

        [HttpPost("{id}/accounts/{playerId}")]
        public async Task<IActionResult> AddCrAccount(Guid id, string playerId)
        {
            try
            {
                if (!Regex.Match(playerId, @"[0289CcGgJjLlPpQqRrUuVvYy]*").Success)
                {
                    return BadRequest();
                }

                if (await _crAccountService.GetIfExistsByPlayerTagAsync(playerId))
                {
                    return BadRequest(new {exists = true});
                }

                var user = await _userService.GetByIdAsync(id);
                var crAccount = new CrAccount
                {
                    PlayerId = playerId,
                    IsVerified = false,
                    User = user
                };

                await _crAccountService.AddAsync(crAccount);
                await _crAccountService.SaveAllAsync();

                return Ok(await _crAccountService.GetByUserIdAndPlayerIdAsync(id, playerId));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpDelete("{id}/accounts/{playerId}")]
        public async Task<IActionResult> DeleteCrAccount(Guid id, string playerId)
        {
            try
            {
                var crAccount = await _crAccountService.GetByUserIdAndPlayerIdAsync(id, playerId);
                var user = await _userManager.GetUserAsync(User);

                if (!await _userManager.IsInRoleAsync(user, "Fejlesztő") && crAccount.User.Id != user.Id)
                {
                    return Forbid();
                }

                _crAccountService.Remove(crAccount);
                await _crAccountService.SaveAllAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // [AllowAnonymous]
        [HttpPost("{id}/accounts/{playerId}/verify")]
        public async Task<IActionResult> VerifyCrAccount(Guid id, string playerId)
        {
            try
            {
                var crAccount = await _crAccountService.GetByUserIdAndPlayerIdAsync(id, playerId);
                var user = await _userManager.GetUserAsync(User);

                if (!await _userManager.IsInRoleAsync(user, "Fejlesztő") && crAccount.User.Id != user.Id)
                {
                    return Forbid();
                }

                var relatedCrAccounts = await _crAccountService.GetByUserIdAsync(id);
                
                if (!relatedCrAccounts.IsNullOrEmpty())
                {
                    return BadRequest(new {message = "Ezt a fiókot már megerősítették."});
                }
                
                crAccount.IsVerified = true;
                await _crAccountService.SaveAllAsync();
                
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            var currentUser = await _userManager.GetUserAsync(User);

            if (user.Id == currentUser.Id)
            {
                return BadRequest();
            }

            _userService.Remove(user);
            await _userService.SaveAllAsync();

            return Ok();
        }

        [HttpGet("{id}/roles")]
        public async Task<IActionResult> GetUserRoles(Guid id)
        {
            return Ok(await _userManager.GetRolesAsync(await _userService.GetByIdAsync(id)));
        }
    }
}