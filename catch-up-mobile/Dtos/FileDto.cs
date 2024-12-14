
using System.Text.Json.Serialization;

namespace catch_up_mobile.Dtos
{
    public class FileDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("type")]
        public string? Type { get; set; }
        [JsonPropertyName("source")]
        public string? Source { get; set; }
    }
}
