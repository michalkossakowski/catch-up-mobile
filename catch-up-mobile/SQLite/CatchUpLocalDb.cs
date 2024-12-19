using SQLite;
using catch_up_mobile.Dtos;

namespace catch_up_mobile.SQLite
{
    public class CatchUpLocalDb
    {
        private readonly SQLiteAsyncConnection _database;

        public CatchUpLocalDb(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);

            // TABLES
            _database.CreateTableAsync<FaqDto>().Wait();
            _database.CreateTableAsync<FeedbackDto>().Wait();
            _database.CreateTableAsync<CompanyCityDto>().Wait();
        }
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

        // Feedback
        public Task<List<FeedbackDto>> GetFeedbacksAsync()
        {
            return _database.Table<FeedbackDto>().ToListAsync();
        }

        public Task<int> AddFeedbackAsync(FeedbackDto feedback)
        {
            return _database.InsertAsync(feedback);
        }

        public Task<int> UpdateFeedbackAsync(FeedbackDto feedback)
        {
            return _database.UpdateAsync(feedback);
        }

        public Task<int> DeleteFeedbackAsync(FeedbackDto feedback)
        {
            return _database.DeleteAsync(feedback);
        }

        public Task DeleteAllFeedbacksAsync()
        {
            return _database.DeleteAllAsync<FeedbackDto>();
        }

    }
}
