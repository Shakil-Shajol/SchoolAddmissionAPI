using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.ViewModels
{
    public class CandidateResultAddVM
    {
        public int ExamId { get; set; }
        public int CandidateId { get; set; }
        public List<SubjectWiseMark> SubjectWiseResults { get; set; }
    }

    public class SubjectWiseMark
    {
        public int SubjectId { get; set; }
        public double ObtainMark { get; set; }
        public bool IsPass { get; set; }
    }
}
