namespace catch_up_mobile.Dtos
{
    public class MaterialDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public List<FileDto>? Files { get; set; }
    }
}
