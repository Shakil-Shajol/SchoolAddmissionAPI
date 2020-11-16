using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class Exam
    {

        public int ExamId { get; set; }
        public string ExamCode { get; set; }
        public DateTime ExamDate { get; set; }
        public int FullMark { get; set; }
        public int PassMark { get; set; }
        [ForeignKey("Session")]
        public int SessionId { get; set; }

        public virtual Session Session { get; set; }
        public virtual ICollection<ExamSubject> Subjects { get; set; }
        public virtual ICollection<RoomCandidate> Candidates { get; set; }
        public virtual ICollection<SelectedCandidate> SelectedCandidates { get; set; }
        public virtual ICollection<SubjectWiseResult> SubjectWiseResults { get; set; }

    }
}
