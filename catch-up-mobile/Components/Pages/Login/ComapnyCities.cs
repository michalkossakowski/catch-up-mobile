public static class CompanyCities
{
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
}