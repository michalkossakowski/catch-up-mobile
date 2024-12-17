namespace catch_up_mobile.Dtos
{
    public class SchoolingDto
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public Guid CreatorId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
    }

}
