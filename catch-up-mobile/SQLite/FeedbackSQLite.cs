using catch_up_mobile.Dtos;

namespace catch_up_mobile.SQLite
{
    partial class CatchUpDbContext
    {
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