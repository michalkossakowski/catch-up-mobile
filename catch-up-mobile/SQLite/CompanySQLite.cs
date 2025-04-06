using catch_up_mobile.Dtos;

namespace catch_up_mobile.SQLite
{
    partial class CatchUpdbContext
    {
        // CompanyCities
        public Task<List<CompanyCityDto>> GetCitiesAsync()
        {
            return _database.Table<CompanyCityDto>().ToListAsync();
        }

        public Task<int> AddCityAsync(CompanyCityDto city)
        {
            return _database.InsertAsync(city);
        }

        public Task<int> UpdateCityAsync(CompanyCityDto city)
        {
            return _database.UpdateAsync(city);
        }

        public Task<int> DeleteCityAsync(CompanyCityDto city)
        {
            return _database.DeleteAsync(city);
        }

        public Task DeleteAllCitiesAsync()
        {
            return _database.DeleteAllAsync<CompanyCityDto>();
        }
    }
}