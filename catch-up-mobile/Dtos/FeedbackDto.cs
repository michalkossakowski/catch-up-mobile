using catch_up_mobile.Enums;
using SQLite;

namespace catch_up_mobile.Dtos
{
    public class FeedbackDto
    {
        [PrimaryKey]
        public int Id { get; set; }
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public ResourceTypeEnum ResourceType { get; set; }
        public int ResourceId { get; set; }
        public string? ResourceName { get; set; }
        public string? UserName { get; set; }
        public DateTime createdDate { get; set; }
    }
}
