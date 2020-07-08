using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
using backend.DTOs;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class CardsController : ControllerBase
    {
        private readonly IGameDataService _gameDataService;

        public CardsController(IGameDataService gameDataService)
        {
            _gameDataService = gameDataService;
        }
        
        [HttpGet]
        public IActionResult GetCards([FromQuery] int? limit)
        {
            return Ok(_gameDataService.GetCards(limit));
        }
        
        /*
        [HttpGet("level")]
        public IActionResult GetCardLevel(CardLevelDto cardLevelDto)
        {
            var commons = new List<string>
            {
                "Royal Recruits",
                "Royal Giant",
                "Elite Barbarians",
                "Barbarians",
                "Minion Horde",
                "Rascals",
                "Knight",
                "Archers",
                "Minions",
                "Bomber",
                "Goblin Gang",
                "Skeleton Barrel",
                "Firecracker",
                "Goblins",
                "Spear Goblins",
                "Fire Spirits",
                "Bats",
                "Skeletons",
                "Ice Spirit",
                "Mortar",
                "Tesla",
                "Cannon",
                "Arrows",
                "Zap",
                "Giant Snowball"
            };

            var rares = new List<string> 
            {
                "Three Musketeers",
                "Giant",
                "Wizard",
                "Royal Hogs",
                "Valkyrie",
                "Musketeer",
                "Mini P.E.K.K.A",
                "Hog Rider",
                "Battle Ram",
                "Zappies",
                "Flying Machine",
                "Battle Healer",
                "Mega Minion",
                "Dart Goblin",
                "Elixir Golem",
                "Ice Golem",
                "Barbarian Hut",
                "Elixir Collector",
                "Goblin Hut",
                "Inferno Tower",
                "Bomb Tower",
                "Furnace",
                "Goblin Cage",
                "Tombstone",
                "Rocket",
                "Fireball",
                "Earthquake",
                "Heal"
            };

            var epics = new List<string>
            {
                "Golem",
                "P.E.K.K.A",
                "Giant Skeleton",
                "Goblin Giant",
                "Balloon",
                "Witch",
                "Prince",
                "Bowler",
                "Executioner",
                "Cannon Cart",
                "Electro Dragon",
                "Baby Dragon",
                "Dark Prince",
                "Hunter",
                "Skeleton Army",
                "Guards",
                "Wall Breakers",
                "X-Bow",
                "Lightning",
                "Freeze",
                "Poison",
                "Goblin Barrel",
                "Tornado",
                "Clone",
                "Rage",
                "Barbarian Barrel",
                "Mirror"
            };

            var legendaries = new List<string>
            {
                "Lava Hound",
                "Mega Knight",
                "Sparky",
                "Ram Rider",
                "Lumberjack",
                "Inferno Dragon",
                "Electro Wizard",
                "Night Witch",
                "Magic Archer",
                "Ice Wizard",
                "Princess",
                "Miner",
                "Bandit",
                "Royal Ghost",
                "Fisherman",
                "Graveyard",
                "The Log"
            };

            if (commons.Contains(cardLevelDto.CardName))
            {
                return Ok(cardLevelDto.CardLevel);
            }

            if (rares.Contains(cardLevelDto.CardName))
            {
                return Ok(cardLevelDto.CardLevel + 2);
            }

            if (epics.Contains(cardLevelDto.CardName))
            {
                return Ok(cardLevelDto.CardLevel + 5);
            }

            if (legendaries.Contains(cardLevelDto.CardName))
            {
                return Ok(cardLevelDto.CardLevel + 8);
            }

            return BadRequest();
        }
        */
    }
}