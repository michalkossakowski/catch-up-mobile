namespace catch_up_mobile.Dtos
{
    public class SchoolingPartProgressBarDto
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public FileDto? FileIcon { get; set; }
        public bool IsDone { get; set; }
        public string? Title { get; set; }
        public string? ShortDescription { get; set; }
    }
}


