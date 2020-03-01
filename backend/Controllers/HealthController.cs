using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using backend.Data;
using backend.Models;
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
        private readonly ApplicationDbContext _context;

        private readonly CrApiDetails _crApiDetails;

        public HealthController(ApplicationDbContext _context, IOptions<CrApiDetails> crApiDetails)
        {
            this._context = _context;
            this._crApiDetails = crApiDetails.Value;
        }

        private string Query(string url)
        {
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            request.AddHeader("auth", _crApiDetails.ApiKey);
            var response = client.Execute(request).Content;

            return response;
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
            var users = await _context.Users.CountAsync();
            var usersRegisteredToday = await _context.Users.Where(u => u.CreatedAt.Date == DateTime.Today).ToListAsync();

            return Ok(new { usersRegisteredToday, users});
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