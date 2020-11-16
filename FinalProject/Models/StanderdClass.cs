using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class StanderdClass
    {
        [Key]
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public virtual ICollection<Candidate> Candidates { get; set; }
    }
}
