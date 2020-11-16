using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class RoomCandidate
    {
        [Key, Column(Order = 0)]
        [ForeignKey("Exam")]
        public int ExamId { get; set; }
        [Key, Column(Order = 1)]
        [ForeignKey("Candidate")]
        public int CandidateId { get; set; }
        [ForeignKey("Room")]
        public int RoomId { get; set; }
        public virtual Exam Exam { get; set; }
        public virtual Candidate Candidate { get; set; }
        public virtual Room Room { get; set; }

    }
}
