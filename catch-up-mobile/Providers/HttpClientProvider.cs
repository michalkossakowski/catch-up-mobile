using catch_up_mobile.SQLite;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using Microsoft.Maui.Devices;

namespace catch_up_mobile.Providers
{
    public class HttpClientProvider
    {
        private readonly HttpClient _httpClient;
        private CatchUpDbContext _dbContext;
        public HttpClientProvider(CatchUpDbContext dbContext, IConfiguration configuration, HttpClient httpClient)
        {
            _dbContext = dbContext;

            _httpClient = httpClient;
        }

        public HttpClient GetHttpClient()
        {
            return _httpClient;
        }

        public void SetAccessToken(string accessToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        public async Task SetAccessTokenFromDb()
        {
            var accessToken = await _dbContext.GetAccessToken();
            SetAccessToken(accessToken);
        }
    }
}
