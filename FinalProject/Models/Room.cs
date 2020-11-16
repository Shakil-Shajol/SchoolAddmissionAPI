using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class Room
    {
        public int RoomId { get; set; }
        public string RoomNo { get; set; }
        [ForeignKey("ExamCenter")]
        public int CenterId { get; set; }
        public int Capacity { get; set; }
        public virtual ExamCenter ExamCenter { get; set; }
        public virtual ICollection<RoomCandidate> RoomCandidates { get; set; }
    }
}
