using System.Net.Http.Json;
using catch_up_mobile.Dtos;

namespace catch_up_mobile.Services
{
    public class FaqService
    {
        private readonly HttpClient _httpClient;

        public FaqService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<FaqDto>> GetFAQsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<FaqDto>>("api/faq/getall");
        }
    }
}
