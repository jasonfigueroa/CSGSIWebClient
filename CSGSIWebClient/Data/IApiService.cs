using CSGSIWebClient.Models;
using System.Threading.Tasks;

namespace CSGSIWebClient.Data
{
    public interface IApiService
    {
        JWT LoginUser(User user);
        Task<SteamId> GetSteamIdAsync(JWT jwt);
        void RegisterUser(RegisterRequest request);
        bool IsUsernameInDb(string username);
        bool IsSteamIdInDb(string steamId);
    }
}
