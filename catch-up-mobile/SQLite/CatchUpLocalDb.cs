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
            _database.CreateTableAsync<FingerprintCredentials>().Wait();
            _database.CreateTableAsync<UserDto>().Wait();
            _database.CreateTableAsync<FontSizeDto>().Wait();
            _database.CreateTableAsync<LocalizationSettingDto>().Wait();
            _database.CreateTableAsync<FullTask>().Wait();
            _database.CreateTableAsync<UserName>().Wait();
            _database.CreateTableAsync<TaskContentDto>().Wait();
            _database.CreateTableAsync<CategoryDto>().Wait();
            _database.CreateTableAsync<MaterialDto>().Wait();

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

        //Users
        public Task<UserDto> GetUserAsync()
        {
            return _database.Table<UserDto>().FirstOrDefaultAsync();
        }

        public async Task<int> SaveUserAsync(UserDto user)
        {
            // we want only one user logged in so we clear all first
            await _database.DeleteAllAsync<UserDto>();
            return await _database.InsertAsync(user);
        }

        public Task ClearUserAsync()
        {
            return _database.DeleteAllAsync<UserDto>();
        }
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
        // UsersName
        public Task<UserName> GetUserNameAsync(Guid userId)
        {
            return _database.Table<UserName>().FirstAsync(p => p.Id == userId);
        }
        public Task<int> AddUserNameAsync(UserName userName)
        {
            return _database.InsertAsync(userName);
        }
        public Task<int> UpdateUserNameAsync(UserName userName)
        {
            return _database.UpdateAsync(userName);
        }
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

        // LocalizationSetting
        public Task<LocalizationSettingDto> GetLocalizationSettingAsync()
        {
            return _database.Table<LocalizationSettingDto>().FirstOrDefaultAsync();
        }

        public async Task<int> SaveLocalizationSettingAsync(bool isLocalizationRestricted)
        {
            await _database.DeleteAllAsync<LocalizationSettingDto>();

            var setting = new LocalizationSettingDto
            {
                IsRestricted = isLocalizationRestricted
            };

            return await _database.InsertAsync(setting);
        }

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
