using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FinalProject.Models;
using FinalProject.Repository;
using FinalProject.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamsController : ControllerBase
    {
        private readonly IAdmissionRepo<Exam> _repo;
        private readonly IAdmissionRepo<Session> _sessionRepo;
        private readonly IAdmissionRepo<ExamSubject> _esRepo;
        private readonly IMapper _mapper;

        public ExamsController(IAdmissionRepo<Exam> repo,IAdmissionRepo<Session> sessionRepo,IAdmissionRepo<ExamSubject> esRepo,IMapper mapper)
        {
            _repo = repo;
            _sessionRepo = sessionRepo;
            _esRepo = esRepo;
            _mapper = mapper;
        }

        // GET: api/Exams
        [HttpGet]
        public async Task<IEnumerable<Exam>> GetExams()
        {
            List<Exam> examsToReturn = new List<Exam>();
            var exams= await _repo.GetT();
            foreach (var exam in exams)
            {
                var session = await _sessionRepo.GetTById(exam.SessionId);
                session.Exams = null;
                exam.Session = session;
                examsToReturn.Add(exam);
            }
            return examsToReturn;
        }

        // GET: api/Exams/1
        [HttpGet("{id}")]
        public async Task<ActionResult> GetExam([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var exam = await _repo.GetTById(id);
            if (exam == null)
            {
                return NotFound();
            }
            var session = await _sessionRepo.GetTById(exam.SessionId);
            session.Exams = null;
            exam.Session = session;
            return Ok(exam);
        }

        // POST: api/Exams
        [HttpPost]
        public async Task<ActionResult> PostExam([FromBody] ExamAddViewModel exam)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Exam examToPost = new Exam();
            examToPost.ExamId = exam.ExamId;
            examToPost.ExamDate = exam.ExamDate;
            examToPost.ExamCode = exam.ExamCode;
            examToPost.FullMark = exam.FullMark;
            examToPost.PassMark = exam.PassMark;
            examToPost.SessionId = exam.SessionId;
            _repo.Add(examToPost);
            var save = await _repo.SaveAsync(examToPost);
            foreach (var item in exam.Subjects)
            {
                ExamSubject es = new ExamSubject();
                es.ExamId = save.ExamId;
                es.SubjectId = item.SubjectId;
                es.Mark = item.Mark;
                es.PassMark = item.PassMark;
                _esRepo.Add(es);
                var esp=await _esRepo.SaveAsync(es);
                examToPost.Subjects.Add(es);
            }
            

            return Ok();
        }

        // PUT: api/Exams/1
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExam([FromRoute] int id, [FromBody] Exam exam)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != exam.ExamId)
            {
                return BadRequest();
            }
            try
            {
                _repo.Update(exam);
                var save = await _repo.SaveAsync(exam);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }


        // DELETE: api/Exams/1
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteExam([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var exam = await _repo.GetTById(id);
            if (exam == null)
            {
                return NotFound();
            }
            _repo.Delete(exam);
            await _repo.SaveAsync(exam);
            return Ok(exam);
        }


        private bool ExamExists(int id)
        {
            var exam = _repo.GetTById(id);
            if (exam == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}