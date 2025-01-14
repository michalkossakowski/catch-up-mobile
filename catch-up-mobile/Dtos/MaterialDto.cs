using SQLite;

namespace catch_up_mobile.Dtos
{
    public class MaterialDto
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string? Name { get; set; }
        [Ignore]
        public List<FileDto>? Files { get; set; } = new List<FileDto>();
    }
}
