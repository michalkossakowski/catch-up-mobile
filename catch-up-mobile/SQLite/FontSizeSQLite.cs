using catch_up_mobile.Dtos;

namespace catch_up_mobile.SQLite
{
    partial class CatchUpDbContext
    {
        //Font Size
        public Task<FontSizeDto> GetFontSizeAsync()
        {
            return _database.Table<FontSizeDto>().FirstOrDefaultAsync();
        }

        public Task<int> SaveFontSizeAsync(FontSizeDto fontSize)
        {
            var existingFontSize = _database.Table<FontSizeDto>().FirstOrDefaultAsync().Result;
            if (existingFontSize != null)
            {
                existingFontSize.FontSize = fontSize.FontSize;
                return _database.UpdateAsync(existingFontSize);
            }
            else
            {
                return _database.InsertAsync(fontSize);
            }
        }
    }
}