using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.ViewModels
{
    public class BulkCandidateWiseResult
    {
        public int ExamId { get; set; }
        public List<ShortCandidateDetails> Candidates { get; set; }
    }

    public class ShortCandidateDetails
    {
        public int CandidateId { get; set; }
        public string FullName { get; set; }
        public List<SubjectWise> Subjects { get; set; }
    }

    public class SubjectWise
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public int ObtainMark { get; set; }
    }

}
