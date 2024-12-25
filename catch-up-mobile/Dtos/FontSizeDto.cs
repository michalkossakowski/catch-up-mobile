
using SQLite;

namespace catch_up_mobile.Dtos
{
    public class FontSizeDto
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string FontSize { get; set; }
    }
}
