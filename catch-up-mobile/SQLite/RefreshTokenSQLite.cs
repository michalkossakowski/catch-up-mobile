using catch_up_mobile.Dtos;

namespace catch_up_mobile.SQLite
{
    public partial class CatchUpDbContext
    {
        public async Task<string> GetRefreshToken()
        {
            try
            {
                var rt = await _database.Table<RefreshTokenDto>().FirstOrDefaultAsync();
                return rt?.Token;
            }
            catch
            {
                return string.Empty;
            }
        }

        public async Task<int> SaveRefreshToken(string token)
        {
            try
            {
                var existingToken = await _database.Table<RefreshTokenDto>().FirstOrDefaultAsync();
                if (existingToken != null)
                {
                    existingToken.Token = token;
                    return await _database.UpdateAsync(existingToken);
                }
                else
                {
                    var newToken = new RefreshTokenDto(token);
                    return await _database.InsertAsync(newToken);
                }
            }
            catch
            {
                return 0;
            }
        }

        public async Task DeleteRefreshToken()
        {
            try
            {
                var existingToken = await _database.Table<RefreshTokenDto>().FirstOrDefaultAsync();
                if (existingToken != null)
                {
                    await _database.DeleteAsync(existingToken);
                }
            }
            catch
            {
            }
        }
    }
}

