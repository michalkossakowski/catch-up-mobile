using catch_up_mobile.Dtos;

namespace catch_up_mobile.SQLite
{
    partial class CatchUpdbContext
    {

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

    }
}