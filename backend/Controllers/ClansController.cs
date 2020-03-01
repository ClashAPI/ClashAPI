using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
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
        private readonly UserManager<User> _userManager;
        private readonly CrApiDetails _crApiDetails;

        public ClansController(UserManager<User> userManager, IOptions<CrApiDetails> crApiDetails)
        {
            _userManager = userManager;
            _crApiDetails = crApiDetails.Value;
        }

        [ResponseCache(Duration = 47, Location = ResponseCacheLocation.Any)]
        [HttpGet("{id}")]
        public async Task<IActionResult> Clan(string id)
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

            services.AddTransient<IClanClient, ClanClient>();

            var buildServiceProvider = services.BuildServiceProvider();

            var clanClient = buildServiceProvider.GetRequiredService<IClanClient>();

            return Ok(await clanClient.GetClanAsync("#" + id));
        }

        [HttpGet("{id}/followage")]
        public async Task<IActionResult> GetIsFollowingClan(string id)
        {
            var isFollowing = false;

            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                isFollowing = user.ClanFollows.FirstOrDefault(f => f.ClanTag == id) != null;
            }

            return Ok(isFollowing);
        }
    }
}