using SQLite;

namespace catch_up_mobile.Dtos
{
    public class FaqDto
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Answer { get; set; }
        public int? MaterialsId { get; set; }
    }
}
