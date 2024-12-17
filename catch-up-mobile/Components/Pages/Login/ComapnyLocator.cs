using CommunityToolkit.Maui.Alerts;

public static class ComapnyLocator
{
    public static string locationMessage = "";
    private static readonly Dictionary<string, (double Latitude, double Longitude, double RadiusKm)> CitiesCoordinates = new Dictionary<string, (double Latitude, double Longitude, double RadiusKm)>()
    {
        { "Warsaw", (52.2297, 21.0122, 15) },
        { "Krakow", (50.0647, 19.9450, 7) },
        { "Gdansk", (54.3520, 18.6466, 8) },
        { "Wroclaw", (51.1079, 17.0385, 8) },
        { "Poznan", (52.4064, 16.9252, 10) },
        { "Lodz", (51.7592, 19.4560, 10) },
        { "Szczecin", (53.4285, 14.5528, 10) },
        { "Bialystok", (53.1325, 23.1688, 7) },
        { "Katowice", (50.2649, 19.0238, 7) }
    };

    public static Dictionary<string, (double Latitude, double Longitude, double RadiusKm)> GetCities()
    {
        return CitiesCoordinates;
    }
    public static async Task GetLocation()
    {
        Location location = await Geolocation.GetLastKnownLocationAsync();
        if (location == null)
        {
            locationMessage = "Unable to retrieve location";
            return;
        }
        double latitude = location.Latitude;
        double longitude = location.Longitude;
        Toast.Make($"latitude: {latitude} longitude: {longitude}").Show();
        string city = GetCityIfInside(latitude, longitude);
        if (!string.IsNullOrEmpty(city))
        {
            locationMessage = $"You are in {city}";
        }
        else
        {
            locationMessage = "You are out of company cities";
        }
    }

    private static string GetCityIfInside(double latitude, double longitude)
    {
        foreach (var city in CitiesCoordinates)
        {
            string cityName = city.Key;
            double cityLatitude = city.Value.Latitude;
            double cityLongitude = city.Value.Longitude;
            double cityRadius = city.Value.RadiusKm;

            double distance = CalculateDistance(latitude, longitude, cityLatitude, cityLongitude);

            if (distance <= cityRadius)
            {
                return cityName;
            }
        }
        return "";
    }

    //formuła Haversine'a https://en.wikipedia.org/wiki/Haversine_formula jakby ktoś chciał zrozumieć
    private static double CalculateDistance(double ourLatitude, double ourLongitude, double cityLatitude, double cityLongitude)
    {
        const double EarthRadiusKm = 6371.0;

        double LatitudeDistance = DegreesToRadians(cityLatitude - ourLatitude);
        double LongitudeDistance = DegreesToRadians(cityLongitude - ourLongitude);

        double a = Math.Sin(LatitudeDistance / 2) * Math.Sin(LatitudeDistance / 2) +
                   Math.Cos(DegreesToRadians(ourLatitude)) * Math.Cos(DegreesToRadians(cityLatitude)) *
                   Math.Sin(LongitudeDistance / 2) * Math.Sin(LongitudeDistance / 2);

        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a)); //kąt który powstaje między 2 punktami na kuli

        return EarthRadiusKm * c;
    }
    private static double DegreesToRadians(double degrees)
    {
        return degrees * Math.PI / 180;
    }
}