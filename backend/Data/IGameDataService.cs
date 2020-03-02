using System.Collections.Generic;
using System.Threading.Tasks;
using Pekka.ClashRoyaleApi.Client.Models.ClanModels;
using Pekka.ClashRoyaleApi.Client.Models.PlayerModels;

namespace backend.Data
{
    public interface IGameDataService
    {
        object GetPlayer(string playerTag);
        Task<List<PlayerBattleLog>> GetPlayerBattlesAsync(string playerTag);
        Task<PlayerUpcomingChest[]> GetPlayerChestsAsync(string playerTag);
        Task<Clan> GetClanAsync(string clanTag);
    }
}