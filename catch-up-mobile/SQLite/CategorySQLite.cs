using catch_up_mobile.Dtos;

namespace catch_up_mobile.SQLite
{
    partial class CatchUpDbContext
    {
        // Category methods
        public Task<List<CategoryDto>> GetCategoriesAsync()
        {
            return _database.Table<CategoryDto>().ToListAsync();
        }

        public Task<int> AddCategoryAsync(CategoryDto category)
        {
            return _database.InsertAsync(category);
        }

        public Task DeleteAllCategoriesAsync()
        {
            return _database.DeleteAllAsync<CategoryDto>();
        }

    }
}