using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Pekka.ClashRoyaleApi.Client.Clients;
using Pekka.ClashRoyaleApi.Client.Contracts;
using Pekka.Core;
using Pekka.Core.Contracts;
using RestSharp;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class PlayersController : ControllerBase
    {
        private readonly IGameDataService _gameDataService;
        private readonly IRepository _repository;

        public PlayersController(IGameDataService gameDataService, IRepository repository)
        {
            _gameDataService = gameDataService;
            _repository = repository;
        }

        [ResponseCache(Duration = 51)]
        [HttpGet("{id}")]
        public IActionResult Player(string id)
        {
            return Ok(_gameDataService.GetPlayer(id));
        }

        [HttpGet("{id}/followage")]
        public async Task<IActionResult> GetIsFollowingPlayer(string id)
        {
            return Ok(_repository.GetIsFollowingPlayerAsync(id, User));
        }

        [ResponseCache(Duration = 51)]
        [HttpGet("{id}/chests")]
        public async Task<IActionResult> PlayerChests(string id)
        {
            return Ok(new {model = await _gameDataService.GetPlayerChestsAsync(id)});
        }

        [ResponseCache(Duration = 51)]
        [HttpGet("{id}/battles")]
        public async Task<IActionResult> PlayerBattles(string id)
        {
            return Ok(await _gameDataService.GetPlayerBattlesAsync(id));
        }

        [HttpGet("{id}/badges")]
        public async Task<IActionResult> GetUserBadges(string id)
        {
            return Ok(await _repository.GetBadgesAsync(id));
        }
    }
}