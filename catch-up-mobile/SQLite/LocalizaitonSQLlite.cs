using catch_up_mobile.Dtos;

namespace catch_up_mobile.SQLite
{
    partial class CatchUpdbContext
    {

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
    }
}