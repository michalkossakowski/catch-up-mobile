using catch_up_mobile.SQLite;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace catch_up_mobile.Handlers
{
    public class AuthenticationHandler : DelegatingHandler
    {
        private readonly CatchUpDbContext _dbContext;
        private readonly IConfiguration _configuration;
        private static SemaphoreSlim _refreshSemaphore = new SemaphoreSlim(1, 1);

        public AuthenticationHandler(CatchUpDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.RequestUri?.AbsolutePath.Contains("/api/Auth/Login") == true ||
                request.RequestUri?.AbsolutePath.Contains("/api/Auth/Refresh") == true)
            {
                return await base.SendAsync(request, cancellationToken);
            }

            var accessToken = await _dbContext.GetAccessToken();

            if (!string.IsNullOrEmpty(accessToken))
            {
                if (IsTokenExpired(accessToken))
                {
                    accessToken = await RefreshTokenAsync();
                }

                if (!string.IsNullOrEmpty(accessToken))
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                }
            }

            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                var newToken = await RefreshTokenAsync();
                if (!string.IsNullOrEmpty(newToken))
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", newToken);
                    response = await base.SendAsync(request, cancellationToken);
                }
            }

            return response;
        }

        private bool IsTokenExpired(string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);
                var expClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "exp");

                if (expClaim != null && long.TryParse(expClaim.Value, out long expSeconds))
                {
                    var expiryDate = DateTimeOffset.FromUnixTimeSeconds(expSeconds).DateTime;
                    return DateTime.UtcNow >= expiryDate.AddMinutes(-1);
                }

                return true;
            }
            catch
            {
                return true;
            }
        }

        private async Task<string> RefreshTokenAsync()
        {
            await _refreshSemaphore.WaitAsync();
            try
            {
                var currentToken = await _dbContext.GetAccessToken();
                if (!string.IsNullOrEmpty(currentToken) && !IsTokenExpired(currentToken))
                {
                    return currentToken;
                }

                var refreshToken = await GetRefreshTokenFromDb();
                if (string.IsNullOrEmpty(refreshToken))
                {
                    return null;
                }

                var baseAddress = _configuration["ApiSettings:Url"];
                
                var httpClientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };
                
                using var httpClient = new HttpClient(httpClientHandler) { BaseAddress = new Uri(baseAddress) };
                
                var content = new StringContent($"\"{refreshToken}\"", System.Text.Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync("api/Auth/Refresh", content);
                
                if (response.IsSuccessStatusCode)
                {
                    var tokens = await response.Content.ReadFromJsonAsync<TokenResponse>();
                    if (tokens != null)
                    {
                        await _dbContext.SaveAccessToken(tokens.AccessToken);
                        await SaveRefreshTokenToDb(tokens.RefreshToken);
                        
                        JwtTokens.SetTokens(tokens.AccessToken, tokens.RefreshToken);
                        
                        return tokens.AccessToken;
                    }
                }

                await _dbContext.DeleteAccessToken();
                await ClearRefreshTokenFromDb();
                return null;
            }
            catch
            {
                return null;
            }
            finally
            {
                _refreshSemaphore.Release();
            }
        }

        private async Task<string> GetRefreshTokenFromDb()
        {
            try
            {
                return await _dbContext.GetRefreshToken();
            }
            catch
            {
                return null;
            }
        }

        private async Task SaveRefreshTokenToDb(string refreshToken)
        {
            try
            {
                await _dbContext.SaveRefreshToken(refreshToken);
            }
            catch
            {
            }
        }

        private async Task ClearRefreshTokenFromDb()
        {
            try
            {
                await _dbContext.DeleteRefreshToken();
            }
            catch
            {
            }
        }

        private class TokenResponse
        {
            public string AccessToken { get; set; }
            public string RefreshToken { get; set; }
        }
    }
}

