using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class Session
    {
        public int SessionId { get; set; }
        public string SessionYear { get; set; }
        public virtual ICollection<Exam> Exams { get; set; }
    }
}
