using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProject.Models;
using FinalProject.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Repository
{
    public class SqlResultRepo : IResultRepo
    {
        private readonly AddmissionContext _context;

        public SqlResultRepo(AddmissionContext context)
        {
            _context = context;
        }

        public void Addresult(CandidateResultAddVM entity)
        {
            foreach (var item in entity.SubjectWiseResults)
            {
                SubjectWiseResult res = new SubjectWiseResult { CandidateId = entity.CandidateId, ExamId = entity.ExamId, SubjectId = item.SubjectId, ObtainMarks = item.ObtainMark, IsPass = IsPass(item.ObtainMark, entity.ExamId, item.SubjectId) };
                _context.SubjectWiseResults.Add(res);
            }
        }

        private bool IsPass(double obtainMark,int examId,int subjectId)
        {
            var context = _context.ExamSubjects.FirstOrDefault(x => x.ExamId == examId && x.SubjectId == subjectId);
            if (context.PassMark > obtainMark)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public int CountCandidatesInRoom(int roomId,int examId)
        {
            return _context.RoomCandidates.Where(x => x.RoomId == roomId && x.ExamId==examId).ToList().Count();
        }

        public async Task<CandidateResultAddVM> SubjectWiseResults(int examId, int candidateId)
        {
            //List<SubjectWiseResult> results = await _context.SubjectWiseResults.Where(x => x.ExamId == examId && x.CandidateId == candidateId).ToListAsync();
            //return results;
            List<SubjectWiseMark> subjectWiseMarks = new List<SubjectWiseMark>();
            List<SubjectWiseResult> results = await _context.SubjectWiseResults.Where(x => x.ExamId == examId && x.CandidateId == candidateId).ToListAsync();
            foreach (var item in results)
            {
                SubjectWiseMark mark = new SubjectWiseMark
                {
                    SubjectId = item.SubjectId,
                    ObtainMark = item.ObtainMarks,
                    IsPass = item.IsPass
                };
                subjectWiseMarks.Add(mark);
            }
            CandidateResultAddVM resultOutput = new CandidateResultAddVM { ExamId = examId, CandidateId = candidateId, SubjectWiseResults = subjectWiseMarks };
            return resultOutput;
        }

        public async Task GenarateResult(GenarateResultVM genarateResult)
        {
            List<GenarateResultVM> genarateResults = new List<GenarateResultVM>();
            List<int> candidatsIds = await _context.SubjectWiseResults.Include("Candidate").Where(x => x.ExamId == genarateResult.ExamId && x.Candidate.StanderdClassId==genarateResult.StandardId).Select(x => x.CandidateId).Distinct().ToListAsync();
            List<CandateWiseResult> candateWiseResults = new List<CandateWiseResult>();
            foreach (var id in candidatsIds)
            {
                var items = _context.SubjectWiseResults.Where(x => x.CandidateId == id && x.ExamId == genarateResult.ExamId);
                CandateWiseResult cwr = new CandateWiseResult { CandidateId = id, ExamId = genarateResult.ExamId,IsPass=true,ObtainMarks=0 };
                foreach (var item in items)
                {
                    cwr.ObtainMarks += item.ObtainMarks;
                    if (cwr.IsPass)
                    {
                        cwr.IsPass = item.IsPass;
                    }
                    else
                    {
                        cwr.IsPass = false;
                    }
                }
                candateWiseResults.Add(cwr);
            }
            candateWiseResults = candateWiseResults.OrderByDescending(x => x.ObtainMarks).ToList();
            List<CandateWiseResult> candidatesToPost = new List<CandateWiseResult>();
            candidatesToPost = candateWiseResults.Take(genarateResult.SelectedCandidateNo).ToList();
            foreach (var item in candidatesToPost)
            {
                SelectedCandidate s = new SelectedCandidate
                {
                    ExamId = genarateResult.ExamId,
                    CandidateId = item.CandidateId,
                    IsAdmitted = false,
                    IsInWatingList = false
                };
                _context.SelectedCandidates.Add(s);
            }
            _context.SaveChanges();
            candidatesToPost.Clear();
            candidatesToPost = candateWiseResults.Skip(genarateResult.SelectedCandidateNo).Take(genarateResult.WaitingListNo).ToList();
            foreach (var item in candidatesToPost)
            {
                SelectedCandidate s = new SelectedCandidate
                {
                    ExamId = genarateResult.ExamId,
                    CandidateId = item.CandidateId,
                    IsAdmitted = false,
                    IsInWatingList = true
                };
                _context.SelectedCandidates.Add(s);
            }
            _context.SaveChanges();
        }

        public async Task<Notice> LatestNotice()
        {
           return await _context.Notices.OrderByDescending(x => x.PublishDate).ThenByDescending(x=>x.NoticeID).FirstOrDefaultAsync();
        }

        public Task<IEnumerable<SelectedCandidate>> SelectedCandidates(int ExamId)
        {
            throw new NotImplementedException();
        }

        public async Task<GetStudentToResultEntryViewModel> GetCandidatesByExamAndStandardId(int examId, int standardId)
        {
            List<Candidate> candidates = new List<Candidate>();
            var studentByExam =await _context.RoomCandidates.Where(x => x.ExamId == examId).Include("Candidate").ToListAsync();


            foreach (var item in studentByExam)
            {
                if (item.Candidate.StanderdClassId==standardId)
                {
                    item.Candidate.Exams = null;
                    candidates.Add(item.Candidate);
                }
                
            }
            List<Subject> subjects = new List<Subject>();
            try
            {
                
                var examSubject = _context.ExamSubjects.Where(x => x.ExamId == examId).Include("Subject").ToList();
                foreach (var item in examSubject)
                {
                    item.Subject.Exams = null;
                    subjects.Add(item.Subject);
                }
            }
            catch (Exception ex)
            {

                string message = ex.Message;
            }


            GetStudentToResultEntryViewModel lstForResult = new GetStudentToResultEntryViewModel();
            lstForResult.ExamId = examId;
            lstForResult.Candidates=candidates;
            lstForResult.Subjects = subjects;


            return lstForResult;
        }
    }
}
