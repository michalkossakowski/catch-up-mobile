using System.Net.Http.Json;
using catch_up_mobile.Models;

namespace catch_up_mobile.Services
{
    public class FAQService
    {
        private readonly HttpClient _httpClient;

        public FAQService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<FAQ>> GetFAQsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<FAQ>>("api/faq/getall");
        }
    }
}
