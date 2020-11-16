using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class Subject
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public virtual ICollection<ExamSubject> Exams { get; set; }
        public virtual ICollection<SubjectWiseResult> SubjectWiseResults { get; set; }
    }
}
