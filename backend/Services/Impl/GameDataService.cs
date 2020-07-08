using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Pekka.ClashRoyaleApi.Client.Clients;
using Pekka.ClashRoyaleApi.Client.Contracts;
using Pekka.ClashRoyaleApi.Client.Models.ClanModels;
using Pekka.ClashRoyaleApi.Client.Models.PlayerModels;
using Pekka.Core;
using Pekka.Core.Contracts;
using RestSharp;

namespace backend.Services.Impl
{
    public class GameDataService : IGameDataService
    {
        private readonly CrApiDetails _crApiDetails;

        public GameDataService(IOptions<CrApiDetails> crApiDetails)
        {
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
        
        public object GetPlayer(string playerTag)
        {
            return JsonConvert.DeserializeObject(Query("https://proxy.royaleapi.dev/v1/players/%23" + playerTag));
        }

        public async Task<List<PlayerBattleLog>> GetPlayerBattlesAsync(string playerTag)
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
            var playerBattles = await playerClient.GetBattlesAsync("#" + playerTag);

            return playerBattles;
        }

        public async Task<PlayerUpcomingChest[]> GetPlayerChestsAsync(string playerTag)
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
            var playerChests = await playerClient.GetUpcomingChests("#" + playerTag);
            var playerChestsModel = playerChests.Items;

            return playerChestsModel;
        }

        public async Task<Clan> GetClanAsync(string clanTag)
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

            return await clanClient.GetClanAsync("#" + clanTag);
        }

        public object GetPlayerLeaderboard(int? limit = null)
        {
            if (limit != null)
            {
                return JsonConvert.DeserializeObject(Query("https://proxy.royaleapi.dev/v1/locations/global/rankings/players" + "?limit=" + limit));
            }
            
            return JsonConvert.DeserializeObject(Query("https://proxy.royaleapi.dev/v1/locations/global/rankings/players"));
        }

        public object GetCards(int? limit = null)
        {
            if (limit != null)
            {
                return JsonConvert.DeserializeObject(Query("https://proxy.royaleapi.dev/v1/cards" + "?limit=" + limit));
            }
            
            return JsonConvert.DeserializeObject(Query("https://proxy.royaleapi.dev/v1/cards"));
        }
    }
}