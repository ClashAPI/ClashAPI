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
        private readonly ApplicationDbContext _context;
        private readonly CrApiDetails _crApiDetails;
        private readonly UserManager<User> _userManager;

        public PlayersController(UserManager<User> userManager, ApplicationDbContext context, IOptions<CrApiDetails> crApiDetails)
        {
            _userManager = userManager;
            _context = context;
            _crApiDetails = crApiDetails.Value;
        }

        private string Query(string url)
        {
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", string.Format("Bearer {0}", _crApiDetails.ApiKey));
            var response = client.Execute(request).Content;

            return response;
        }

        [ResponseCache(Duration = 51)]
        [HttpGet("{id}")]
        public async Task<IActionResult> Player(string id)
        {
            var player = JsonConvert.DeserializeObject(Query("https://proxy.royaleapi.dev/v1/players/%23" + id));

            return Ok(player);
        }

        [HttpGet("{id}/followage")]
        public async Task<IActionResult> GetIsFollowingPlayer(string id)
        {
            var isFollowing = false;

            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                isFollowing = user.PlayerFollows.FirstOrDefault(f => f.PlayerTag == id) != null;
            }

            return Ok(isFollowing);
        }

        [ResponseCache(Duration = 51)]
        [HttpGet("{id}/chests")]
        public async Task<IActionResult> PlayerChests(string id)
        {
            var apiOptions = new ApiOptions(_crApiDetails.ApiKey, "https://proxy.royaleapi.dev/v1/");

            var services = new ServiceCollection();

            services.AddSingleton(apiOptions);
            services.AddHttpClient<IRestApiClient, RestApiClient>((provider, client) =>
            {
                var options = provider.GetRequiredService<ApiOptions>();
                client.BaseAddress = new Uri(options.BaseUrl);
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", options.BearerToken);
            });

            services.AddTransient<IPlayerClient, PlayerClient>();

            var buildServiceProvider = services.BuildServiceProvider();

            var playerClient = buildServiceProvider.GetRequiredService<IPlayerClient>();
            var playerChests = await playerClient.GetUpcomingChests("#" + id);
            var playerChestsModel = playerChests.Items;

            return Ok(new {model = playerChestsModel});
        }

        [ResponseCache(Duration = 51)]
        [HttpGet("{id}/battles")]
        public async Task<IActionResult> PlayerBattles(string id)
        {
            var apiOptions = new ApiOptions(_crApiDetails.ApiKey, "https://proxy.royaleapi.dev/v1/");

            var services = new ServiceCollection();

            services.AddSingleton(apiOptions);
            services.AddHttpClient<IRestApiClient, RestApiClient>((provider, client) =>
            {
                var options = provider.GetRequiredService<ApiOptions>();
                client.BaseAddress = new Uri(options.BaseUrl);
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", options.BearerToken);
            });

            services.AddTransient<IPlayerClient, PlayerClient>();

            var buildServiceProvider = services.BuildServiceProvider();

            var playerClient = buildServiceProvider.GetRequiredService<IPlayerClient>();
            var playerBattles = await playerClient.GetBattlesAsync("#" + id);

            return Ok(playerBattles);
        }

        [HttpGet("{id}/badges")]
        public async Task<IActionResult> GetUserBadges(string id)
        {
            var badges = new List<string>();
            var account = await _context.CrAccounts.FirstOrDefaultAsync(a => a.PlayerId == id && a.IsVerified);

            if (account == null) return Ok();

            badges.Add("verified");

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == account.User.Id);
            var devRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "Fejlesztő");

            if (user.UserRoles.FirstOrDefault(r => r.Role == devRole) != null) badges.Add("staff");

            return Ok(badges);
        }
    }
}