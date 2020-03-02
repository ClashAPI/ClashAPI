using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using backend.Data;
using backend.Models;
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
        private readonly IRepository _repository;
        private readonly GameDataService _gameDataService;

        public ClansController(IRepository repository, GameDataService gameDataService)
        {
            _repository = repository;
            _gameDataService = gameDataService;
        }

        [ResponseCache(Duration = 47, Location = ResponseCacheLocation.Any)]
        [HttpGet("{id}")]
        public async Task<IActionResult> Clan(string id)
        {
            return Ok(await _gameDataService.GetClanAsync(id));
        }

        [HttpGet("{id}/followage")]
        public async Task<IActionResult> GetIsFollowingClan(string id)
        {
            return Ok(await _repository.GetIsFollowingClanAsync(id, User));
        }
    }
}