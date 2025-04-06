using catch_up_mobile.Dtos;

namespace catch_up_mobile.SQLite
{
    partial class CatchUpdbContext
    {
        // TaskContent methods
        public Task<TaskContentDto> GetTaskContentAsync(int taskId)
        {
            return _database.Table<TaskContentDto>().FirstOrDefaultAsync(t => t.Id == taskId);
        }

        public Task<List<TaskContentDto>> GetTaskContentsAsync()
        {
            return _database.Table<TaskContentDto>().ToListAsync();
        }

        public Task<int> AddTaskContentAsync(TaskContentDto taskContent)
        {
            return _database.InsertAsync(taskContent);
        }

        public Task<int> UpdateTaskContentAsync(TaskContentDto taskContent)
        {
            return _database.UpdateAsync(taskContent);
        }

        public Task<int> DeleteTaskContentAsync(TaskContentDto taskContent)
        {
            return _database.DeleteAsync(taskContent);
        }

        public Task DeleteAllTaskContentsAsync()
        {
            return _database.DeleteAllAsync<TaskContentDto>();
        }
    }
}