using SQLite;

namespace catch_up_mobile.Dtos
{
    public class CategoryDto
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
