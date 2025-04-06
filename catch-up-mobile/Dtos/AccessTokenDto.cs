using SQLite;

namespace catch_up_mobile.Dtos
{
    public class AccessTokenDto
    {
        [PrimaryKey]
        public string Token { get; set; }
        public AccessTokenDto() { }
        public AccessTokenDto(string token)
        {
            Token = token;
        }
    }
}
