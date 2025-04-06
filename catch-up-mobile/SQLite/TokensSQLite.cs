using catch_up_mobile.Dtos;
using Microsoft.Maui.Controls.PlatformConfiguration;

namespace catch_up_mobile.SQLite
{
    public partial class CatchUpdbContext
    {
        // AccessToken table (temporary - waiting for WELL-THOUGHT-OUT token support)
        // (this will probably never happen xd because Kristofer is lazy and stupid)
        public async Task<string> GetAccessToken()
        {
            try
            {
                var at = await _database.Table<AccessTokenDto>().FirstOrDefaultAsync();
                return at.Token;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting access token: {ex.Message}");
                return string.Empty;
            }
        }
        public Task<int> SaveAccessToken(string token)
        {
            var existingToken = _database.Table<AccessTokenDto>().Where(t => t.Token == token).FirstOrDefaultAsync().Result;
            if (existingToken != null)
            {
                existingToken.Token = token;
                return _database.UpdateAsync(existingToken);
            }
            else
            {
                var newToken = new AccessTokenDto(token);
                return _database.InsertAsync(newToken);
            }
        }

        public async Task DeleteAccessToken()
        {
            var existingToken = await _database.Table<AccessTokenDto>().FirstOrDefaultAsync();
            await _database.DeleteAsync(existingToken);
        }
    }
}

