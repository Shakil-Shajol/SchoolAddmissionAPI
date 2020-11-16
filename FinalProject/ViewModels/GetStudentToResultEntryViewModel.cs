using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.ViewModels
{
    public class GetStudentToResultEntryViewModel
    {
        public int ExamId { get; set; }
        public List<Candidate> Candidates { get; set; }
        public List<Subject> Subjects { get; set; }
    }

}
