using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.ViewModels
{
    public class ExamAddViewModel
    {
        public int ExamId { get; set; }
        public string ExamCode { get; set; }
        public DateTime ExamDate { get; set; }
        public int FullMark 
        {
            get
            {
                int x = 0;
                foreach (var item in Subjects)
                {
                    x += item.Mark;
                }
                return x;
            }
        }
        public int PassMark 
        {
            get
            {
                int x = 0;
                foreach (var item in Subjects)
                {
                    x += item.PassMark;
                }
                return x;
            }
        }
        public int SessionId { get; set; }
        public List<ExamSubjectAddViewModel> Subjects { get; set; }

    }
    public class ExamSubjectAddViewModel
    {
        public int SubjectId { get; set; }
        public int Mark { get; set; }
        public int PassMark { get; set; }
    }
}
