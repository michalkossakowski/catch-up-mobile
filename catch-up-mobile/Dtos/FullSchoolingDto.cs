namespace catch_up_mobile.Dtos
{
    public class FullSchoolingDto
    {
        public SchoolingDto Schooling {get; set;}
        public CategoryDto Category { get; set; }
        public List<SchoolingPartDto> Parts { get; set; }

        public FullSchoolingDto(SchoolingDto schooling, CategoryDto category, List<SchoolingPartDto> parts)
        {
            Schooling = schooling;
            Category = category;
            Parts = parts;
        }
    }
}
