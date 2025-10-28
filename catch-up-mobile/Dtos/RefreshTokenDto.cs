using SQLite;

namespace catch_up_mobile.Dtos
{
    public class RefreshTokenDto
    {
        [PrimaryKey]
        public string Token { get; set; }
        public RefreshTokenDto() { }
        public RefreshTokenDto(string token)
        {
            Token = token;
        }
    }
}

