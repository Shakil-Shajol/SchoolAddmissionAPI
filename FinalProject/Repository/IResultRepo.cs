using FinalProject.Models;
using FinalProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Repository
{
    public interface IResultRepo
    {
        int CountCandidatesInRoom(int roomId, int examId);
        Task<CandidateResultAddVM> SubjectWiseResults(int examId, int candidateId);
        void Addresult(CandidateResultAddVM entity);
        Task GenarateResult(GenarateResultVM genarateResult);
        Task<Notice> LatestNotice();
        Task<IEnumerable<SelectedCandidate>> SelectedCandidates(int ExamId);
        Task<GetStudentToResultEntryViewModel> GetCandidatesByExamAndStandardId(int examId, int standardId);
    }
}
