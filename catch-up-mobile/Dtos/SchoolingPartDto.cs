namespace catch_up_mobile.Dtos
{
    public class SchoolingPartDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public List<MaterialDto> Materials { get; set; }

    }
}
