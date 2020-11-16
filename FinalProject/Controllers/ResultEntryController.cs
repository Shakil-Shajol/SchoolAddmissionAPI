using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProject.Models;
using FinalProject.Repository;
using FinalProject.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultEntryController : ControllerBase
    {
        private readonly IAdmissionRepo<SubjectWiseResult> _repo;
        private readonly IResultRepo _candidateRepo;

        public ResultEntryController(IAdmissionRepo<SubjectWiseResult> repo, IResultRepo candidateRepo)
        {
            _repo = repo;
            _candidateRepo = candidateRepo;
        }

        // POST: api/ResultEntry
        [HttpPost]
        public async Task<ActionResult<GetStudentToResultEntryViewModel>> PostResult([FromBody] CanLoadVM criteria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var candidateandsubjectlist= await _candidateRepo.GetCandidatesByExamAndStandardId(criteria.ExamId, criteria.StandardId);
            return Ok(candidateandsubjectlist);
        }
    }
}
