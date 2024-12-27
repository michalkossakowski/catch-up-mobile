using System;
using System.Text.Json.Serialization;
using SQLite;

namespace catch_up_mobile.Dtos
{
    public class CompanyCityDto
    {
        [PrimaryKey]
        public string cityName { get; set; }
        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }
        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }
        [JsonPropertyName("radiusKm")]
        public double RadiusKm { get; set; }
    }
}
