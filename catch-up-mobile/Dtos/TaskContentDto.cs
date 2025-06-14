﻿using SQLite;

namespace catch_up_mobile.Dtos
{
    public class TaskContentDto
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public Guid? CreatorId { get; set; }
        public int? CategoryId { get; set; }
        public int? MaterialsId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        public TaskContentDto() { }
        
        public TaskContentDto(Guid? creatorId, int? categoryId, int? materialsId, string? title, string? description)
        {
            CreatorId = creatorId;
            CategoryId = categoryId;
            MaterialsId = materialsId;
            Title = title;
            Description = description;
        }
    }
}