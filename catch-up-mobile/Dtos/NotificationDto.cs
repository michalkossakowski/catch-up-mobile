using SQLite;

namespace catch_up_mobile.Dtos
{
    public class NotificationDto
    {
        [PrimaryKey]
        public int NotificationId { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime SendDate { get; set; }
        public string Source { get; set; }
        public bool IsRead { get; set; }
    }

    public class NotificationResponse
    {
        public List<NotificationDto> Notifications { get; set; }
        public int TotalCount { get; set; }
    }

    public class NotificationCount
    {
        [PrimaryKey]
        public int TotalCount { get; set; }
    }

}