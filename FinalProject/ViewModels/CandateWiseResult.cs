using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.ViewModels
{
    public class CandateWiseResult
    {
        public int CandidateId { get; set; }
        public int ExamId { get; set; }
        public double ObtainMarks { get; set; }
        public bool IsPass { get; set; }
    }
}
