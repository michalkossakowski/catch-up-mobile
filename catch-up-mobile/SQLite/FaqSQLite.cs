using catch_up_mobile.Dtos;

namespace catch_up_mobile.SQLite
{
    partial class CatchUpdbContext
    {
        // FAQ
        public Task<List<FaqDto>> GetFaqsAsync()
        {
            return _database.Table<FaqDto>().ToListAsync();
        }

        public Task<int> AddFaqAsync(FaqDto faq)
        {
            return _database.InsertAsync(faq);
        }

        public Task<int> UpdateFaqAsync(FaqDto faq)
        {
            return _database.UpdateAsync(faq);
        }

        public Task<int> DeleteFaqAsync(FaqDto faq)
        {
            return _database.DeleteAsync(faq);
        }

        public Task DeleteAllFaqsAsync()
        {
            return _database.DeleteAllAsync<FaqDto>();
        }
    }
}