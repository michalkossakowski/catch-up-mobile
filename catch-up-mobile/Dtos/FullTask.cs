using catch_up_mobile.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace catch_up_mobile.Dtos
{
    internal class FullTask
    {
        public int Id { get; set; }
        public Guid? NewbieId { get; set; }
        public int? MaterialsId { get; set; }
        public int? CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? RoadMapPointId { get; set; }
        public StatusEnum Status { get; set; }
        public DateTime AssignmentDate { get; set; }
        public DateTime? FinalizationDate { get; set; }
        public DateTime? Deadline { get; set; }
        public int SpendTime { get; set; }
        public int Priority { get; set; }
        public int? Rate { get; set; }
    }
}
