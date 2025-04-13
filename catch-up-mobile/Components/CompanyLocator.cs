using catch_up_mobile.Components.Pages.Login;
using catch_up_mobile.Dtos;
using catch_up_mobile.SQLite;
using CommunityToolkit.Maui.Alerts;
public static class CompanyLocator
{
    public static string locationMessage = "";
    public static Location location;
    private static List<CompanyCityDto> cities;
    public static async Task GetLocation(CatchUpDbContext _dbContext)
    {
        cities = await _dbContext.GetCitiesAsync();
        location = await Geolocation.GetLastKnownLocationAsync();
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

    public static void OpenUserMap()
    {
        location.OpenMapsAsync();
    }
    public static async Task OpenCompanyBuildingMap(double latitude, double longitude)
    {
        Location cityLocation = new Location(latitude,longitude);
        await cityLocation.OpenMapsAsync();
    }
    private static string GetCityIfInside(double latitude, double longitude)
    {
        foreach (var city in cities)
        {
            string cityName = city.cityName;
            double cityLatitude = city.Latitude;
            double cityLongitude = city.Longitude;
            double cityRadius = city.RadiusKm;

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