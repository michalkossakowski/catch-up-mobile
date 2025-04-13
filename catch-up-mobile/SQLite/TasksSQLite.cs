using catch_up_mobile.Dtos;

namespace catch_up_mobile.SQLite
{
    partial class CatchUpDbContext
    {
        //Tasks
        public Task<List<FullTask>> GetAllFullTasksAsync()
        {
            return _database.Table<FullTask>().ToListAsync();
        }
        public Task<List<FullTask>> GetTasksForUserAsync(Guid userId)
        {
            return _database.Table<FullTask>()
                .Where(p => p.AssigningId == userId || p.NewbieId == userId)
                .ToListAsync();
        }
        public Task<FullTask> GetFullTaskAsync(int taskId)
        {
            return _database.Table<FullTask>().FirstAsync(p => p.Id == taskId);
        }
        public Task<int> AddTaskAsync(FullTask fullTask)
        {
            return _database.InsertAsync(fullTask);
        }
        public Task<int> UpdateTaskAsync(FullTask fullTask)
        {
            return _database.UpdateAsync(fullTask);
        }
        public Task<int> DeleteTaskAsync(FullTask fullTask)
        {
            return _database.DeleteAsync(fullTask);
        }
    }
}