using CSGSIWebClient.Models;
using System.Threading.Tasks;

namespace CSGSIWebClient.Data
{
    public interface ISteamApiService
    {
        SteamPlayerResponse GetSteamPlayer(SteamId steamId);
        Task<SteamPlayerResponse> GetSteamPlayerAsync(SteamId steamId);
    }
}
