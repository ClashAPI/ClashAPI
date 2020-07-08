using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using backend.Data;
using backend.Models;
using backend.Repositories;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Pekka.ClashRoyaleApi.Client.Clients;
using Pekka.ClashRoyaleApi.Client.Contracts;
using Pekka.Core;
using Pekka.Core.Contracts;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ClansController : ControllerBase
    {
        private readonly IGameDataService _gameDataService;
        private readonly IFollowService _followService;

        public ClansController(IGameDataService gameDataService, IFollowService followService)
        {
            _gameDataService = gameDataService;
            _followService = followService;
        }

        [ResponseCache(Duration = 47, Location = ResponseCacheLocation.Any)]
        [HttpGet("{id}")]
        public async Task<IActionResult> Clan(string id)
        {
            return Ok(await _gameDataService.GetClanAsync(id));
        }

        [Authorize]
        [HttpGet("{id}/followage")]
        public async Task<IActionResult> GetIsFollowingClan(string id)
        {
            return Ok(await _followService.GetIsFollowingClanByClanTagAndUserAsync(id, User));
        }
    }
}