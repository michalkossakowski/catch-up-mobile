using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace catch_up_mobile.SQLite
{
    public static class JwtTokens
    {
        private static string _accessToken;
        private static string _refreshToken;
        private static DateTime _accessTokenExpiry;
        private static HttpClient _httpClient;

        public static HttpClient HttpClient
        {
            set => _httpClient = value;
        }

        public static string AccessToken
        {
            get => _accessToken;
            set => _accessToken = value;
        }

        public static string RefreshToken
        {
            get => _refreshToken;
            set => _refreshToken = value;
        }

        public static DateTime AccessTokenExpiry
        {
            get => _accessTokenExpiry;
            set => _accessTokenExpiry = value;
        }

        public static void SetTokens(string accessToken, string refreshToken)
        {
            _accessToken = accessToken;
            _refreshToken = refreshToken;

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(accessToken);
            var expClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "exp");

            if (expClaim != null && long.TryParse(expClaim.Value, out long expSeconds))
            {
                _accessTokenExpiry = DateTimeOffset.FromUnixTimeSeconds(expSeconds).DateTime;
            }
            else
            {
                _accessTokenExpiry = DateTime.UtcNow.AddHours(1);
            }

            if (_httpClient != null && !string.IsNullOrEmpty(_accessToken))
            {
                _httpClient.DefaultRequestHeaders.Authorization = 
                    new AuthenticationHeaderValue("Bearer", _accessToken);
            }
        }

        public static void ClearTokens()
        {
            _accessToken = null;
            _refreshToken = null;
            _accessTokenExpiry = DateTime.MinValue;

            if (_httpClient != null)
            {
                _httpClient.DefaultRequestHeaders.Authorization = null;
            }
        }

        public static bool IsAccessTokenValid()
        {
            return !string.IsNullOrEmpty(_accessToken) && DateTime.UtcNow < _accessTokenExpiry;
        }
    }
}