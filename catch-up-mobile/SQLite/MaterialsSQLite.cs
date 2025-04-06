using catch_up_mobile.Dtos;

namespace catch_up_mobile.SQLite
{
    partial class CatchUpdbContext
    {
        // Material methods
        public Task<List<MaterialDto>> GetMaterialsAsync()
        {
            return _database.Table<MaterialDto>().ToListAsync();
        }

        public Task<int> AddMaterialAsync(MaterialDto material)
        {
            return _database.InsertAsync(material);
        }

        public Task DeleteAllMaterialsAsync()
        {
            return _database.DeleteAllAsync<MaterialDto>();
        }

    }
}
