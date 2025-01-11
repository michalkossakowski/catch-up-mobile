using catch_up_mobile.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace catch_up_mobile.Dtos
{
    class TaskDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public Guid? NewbieId { get; set; }
        public Guid? AssigningId { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public int TaskContentId { get; set; }
        public int? RoadMapPointId { get; set; }
        public StatusEnum Status { get; set; }
        public DateTime AssignmentDate { get; set; }
        public DateTime? FinalizationDate { get; set; }
        public DateTime? Deadline { get; set; }
        public double SpendTime { get; set; }
        public int Priority { get; set; }
        public int? Rate { get; set; }


        public TaskDto() { }
        
    }
}
