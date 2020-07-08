using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using backend.Data;
using backend.Models;
using backend.Repositories;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RestSharp;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class HealthController : ControllerBase
    {
        private readonly IUserService _userService;

        public HealthController(IUserService userService)
        {
            _userService = userService;
        }

        // No longer works after March 1, 2020
        /*
        [HttpGet("royaleapi")]
        public IActionResult CheckRoyaleAPIStatus()
        {
            var response = Query("https://api.royaleapi.com/health");
            var result = JsonConvert.DeserializeObject(response);

            return Ok(result);
        }
        */

        // TODO: linux implementation
        [Authorize(Roles = "Fejlesztő")]
        [HttpGet("load")]
        public IActionResult GetServerLoad()
        {
            var cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            var ramCounter = new PerformanceCounter("Memory", "Available MBytes");

            var cpuLoad = cpuCounter.NextValue();
            Thread.Sleep(1000);

            return Ok(new { ram = ramCounter.NextValue(), ramMax = (double) GC.GetGCMemoryInfo().TotalAvailableMemoryBytes / 1048576.0, cpu = Convert.ToInt32(cpuCounter.NextValue()) });
        }

        [Authorize(Roles = "Fejlesztő")]
        [HttpGet("users")]
        public async Task<IActionResult> GetUserCount()
        {
            var users = await _userService.GetAllAsync();
            var userCount = users.Count();
            var usersRegisteredToday = _userService.GetAllRegisteredTodayAsync();

            return Ok(new { usersRegisteredToday, userCount});
        }

        [Authorize(Roles = "Fejlesztő")]
        [HttpPost("rungc")]
        public IActionResult RunGC()
        {
            try
            {
                GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}