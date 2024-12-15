using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace catch_up_mobile.Dtos
{
    public class NewbieMentorDto
    {
        [ForeignKey("NewbieId")]
        public Guid NewbieId { get; set; }
        [ForeignKey("MentorId")]
        public Guid MentorId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int State { get; set; }
        public NewbieMentorDto(Guid newbieId, Guid mentorId)
        {
            this.NewbieId = newbieId;
            this.MentorId = mentorId;
            State = 0;
            StartDate = DateTime.Now;
        }
    }
}
