using System;
using SQLite;

namespace catch_up_mobile.Dtos
{
    public class CompanyCityDto
    {
        [PrimaryKey]
        public string cityName { get; set; }
        public double Latitude {  get; set; }
        public double Longitude { get; set; }
        public double RadiusKm { get; set; }
    }
}
