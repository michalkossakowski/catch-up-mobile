using catch_up_mobile.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace catch_up_mobile.Dtos
{
    public class FeedbackDto
    {
        public int Id { get; set; }
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public ResourceTypeEnum ResourceType { get; set; }
        public int ResourceId { get; set; }
        public DateTime createdDate { get; set; }
    }
}
