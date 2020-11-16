using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class ResultsController : ControllerBase
    {
        private readonly IAdmissionRepo<SubjectWiseResult> _repo;
        private readonly IResultRepo _candidateRepo;

        public ResultsController(IAdmissionRepo<SubjectWiseResult> repo,IResultRepo candidateRepo)
        {
            _repo = repo;
            _candidateRepo = candidateRepo;
        }
        // GET: api/Results
        [HttpGet]
        public async Task<IEnumerable<SubjectWiseResult>> GetResults()
        {
            return await _repo.GetT();
        }

        // GET: api/Results/1/1
        [HttpGet("{examId}/{candidateId}")]
        public async Task<ActionResult> GetResult([FromRoute] int examId, [FromRoute] int candidateId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var results = await _candidateRepo.SubjectWiseResults(examId, candidateId);
            if (results==null)
            {
                return NotFound();
            }
            return Ok(results);
        }

        // POST: api/Results
        [HttpPost]
        public async Task<ActionResult> PostResult([FromBody] CandidateResultAddVM candidateResult)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _candidateRepo.Addresult(candidateResult);
            try
            {
                await _repo.SaveAsync();
            }
            catch (Exception ex)
            {

                return Ok(ex.InnerException);
            }
            return Ok();
        }


        // POST: api/Results/Entry
        [Route("Entry")]
        [HttpPost]
        public async Task<ActionResult> PostResult([FromBody] BulkCandidateWiseResult candidateResult)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            foreach (var item in candidateResult.Candidates)
            {
                CandidateResultAddVM cavm = new CandidateResultAddVM();
                cavm.CandidateId = item.CandidateId;
                cavm.ExamId = candidateResult.ExamId;
                List<SubjectWiseMark> lswvm = new List<SubjectWiseMark>();
                foreach (var it in item.Subjects)
                {
                    SubjectWiseMark swm = new SubjectWiseMark();
                    swm.SubjectId = it.SubjectId;
                    swm.ObtainMark = it.ObtainMark;
                    lswvm.Add(swm);
                }
                cavm.SubjectWiseResults = lswvm;
                _candidateRepo.Addresult(cavm);
                await _repo.SaveAsync();
            }
            return Ok();
        }

        // PUT: api/Results/1
        [HttpPut("{id}")]
        public async Task<IActionResult> PutResult([FromRoute] int id, [FromBody] SubjectWiseResult subjectWiseResult)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != subjectWiseResult.CandidateId)
            {
                return BadRequest();
            }
            try
            {
                _repo.Update(subjectWiseResult);
                var save = await _repo.SaveAsync(subjectWiseResult);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResultExists(id))
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


        // DELETE: api/Results/1
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteResult([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var standard = await _repo.GetTById(id);
            if (standard == null)
            {
                return NotFound();
            }
            _repo.Delete(standard);
            await _repo.SaveAsync(standard);
            return Ok(standard);
        }


        private bool ResultExists(int id)
        {
            var standard = _repo.GetTById(id);
            if (standard == null)
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