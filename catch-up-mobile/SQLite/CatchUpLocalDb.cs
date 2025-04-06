using SQLite;
using catch_up_mobile.Dtos;

namespace catch_up_mobile.SQLite
{
    public partial class CatchUpdbContext
    {
        private readonly SQLiteAsyncConnection _database;

        public CatchUpdbContext(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);

            // TABLES
            _database.CreateTableAsync<FaqDto>().Wait();
            _database.CreateTableAsync<FeedbackDto>().Wait();
            _database.CreateTableAsync<CompanyCityDto>().Wait();
            _database.CreateTableAsync<FingerprintCredentials>().Wait();
            _database.CreateTableAsync<UserDto>().Wait();
            _database.CreateTableAsync<FontSizeDto>().Wait();
            _database.CreateTableAsync<LocalizationSettingDto>().Wait();
            _database.CreateTableAsync<FullTask>().Wait();
            _database.CreateTableAsync<UserName>().Wait();
            _database.CreateTableAsync<TaskContentDto>().Wait();
            _database.CreateTableAsync<CategoryDto>().Wait();
            _database.CreateTableAsync<MaterialDto>().Wait();
            _database.CreateTableAsync<AccessTokenDto>().Wait();
            _database.CreateTableAsync<NotificationDto>().Wait();
            _database.CreateTableAsync<NotificationCount>().Wait();
        }      
    }
}
