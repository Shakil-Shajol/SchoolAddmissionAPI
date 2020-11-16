using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class SubjectWiseResult
    {
        [Key, Column(Order = 0)]
        [ForeignKey("Exam")]
        public int ExamId { get; set; }
        [Key, Column(Order = 1)]
        [ForeignKey("Candidate")]
        public int CandidateId { get; set; }
        [Key, Column(Order = 2)]
        [ForeignKey("Subject")]
        public int SubjectId { get; set; }
        public double ObtainMarks { get; set; }
        public bool IsPass { get; set; }
        public virtual Exam Exam { get; set; }
        public virtual Candidate Candidate { get; set; }
        public virtual Subject Subject { get; set; }
    }
}
