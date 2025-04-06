using catch_up_mobile.SQLite;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;

namespace catch_up_mobile.Providers
{
    public class HttpClientProvider
    {
        private HttpClient _httpClient;
        private CatchUpdbContext _dbContext;
        public HttpClientProvider(CatchUpdbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;

            var httpClientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true 
            };

            _httpClient = new HttpClient(httpClientHandler)
            {
                BaseAddress = new Uri(configuration["ApiSettings:Url"] ?? throw new InvalidOperationException("API Url not found in configuration"))
            };
        }

        public HttpClient GetHttpClient()
        {
            return _httpClient;
        }

        public void SetAccessToken(string accessToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }
    }
}
