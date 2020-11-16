using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class ExamSubject
    {
        [Key, Column(Order = 0)]
        [ForeignKey("Exam")]
        public int ExamId { get; set; }
        [Key, Column(Order = 1)]
        [ForeignKey("Subject")]
        public int SubjectId { get; set; }
        public int Mark { get; set; }
        public int PassMark { get; set; }
        public virtual Exam Exam { get; set; }
        public virtual Subject Subject { get; set; }
    }
}
