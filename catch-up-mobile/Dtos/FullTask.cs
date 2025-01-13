using catch_up_mobile.Enums;
using SQLite;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace catch_up_mobile.Dtos
{
    public class FullTask
    {
        [PrimaryKey]
        public int Id { get; set; }
        public Guid? NewbieId { get; set; }
        public Guid? AssigningId { get; set; }
        public int? MaterialsId { get; set; }
        public int? CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? RoadMapPointId { get; set; }
        public StatusEnum Status { get; set; }
        public DateTime AssignmentDate { get; set; }
        public DateTime? FinalizationDate { get; set; }
        public DateTime? Deadline { get; set; }
        public double SpendTime { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Value must be greater than 0.")]
        public int Priority { get; set; }
        public int? Rate { get; set; }
    }
}
