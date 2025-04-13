using catch_up_mobile.Dtos;

namespace catch_up_mobile.SQLite
{
    public partial class CatchUpDbContext
    {
        //Notifications
        public async Task<List<NotificationDto>> GetNotificationsAsync()
        {
            return await _database.Table<NotificationDto>().OrderByDescending(n => n.NotificationId).ToListAsync();
        }

        public async Task AddNotificationAsync(NotificationDto notification)
        {
            await _database.InsertAsync(notification);
        }

        public async Task<int> AddNotificationsAsync(List<NotificationDto> notifications)
        {
            return await _database.InsertAllAsync(notifications);
        }
        public async Task<int> UpdateNotificationsAsync(List<NotificationDto> notifications)
        {
            return await _database.UpdateAllAsync(notifications);
        }

        public async Task<int> DeleteAllNotificationsAsync()
        {
            return await _database.DeleteAllAsync<NotificationDto>();
        }

        //NotificationCount
        public Task<NotificationCount> GetNotificationCount()
        {
            return _database.Table<NotificationCount>().FirstOrDefaultAsync();
        }
        public Task<int> SaveNotificationCount(int count)
        {
            var existingCount = _database.Table<NotificationCount>().FirstOrDefaultAsync().Result;
            if (existingCount != null)
            {
                existingCount.TotalCount = count;
                return _database.UpdateAsync(existingCount);
            }
            else
            {
                var notificationCount = new NotificationCount
                {
                    TotalCount = count
                };
                return _database.InsertAsync(notificationCount);
            }
        }
    }
}

