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
    public class GenarateResultController : ControllerBase
    {
        private readonly IResultRepo _resultRepo;
        private readonly IAdmissionRepo<SelectedCandidate> _repo;
        private readonly AddmissionContext context;

        public GenarateResultController(IResultRepo resultRepo,IAdmissionRepo<SelectedCandidate> repo,AddmissionContext context)
        {
            _resultRepo = resultRepo;
            _repo = repo;
            this.context = context;
        }


        // POST: api/GenarateResult
        [HttpPost]
        public async Task<ActionResult> PostResult([FromBody] GenarateResultVM genarateResult)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _resultRepo.GenarateResult(genarateResult);
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


        // POST: api/GenarateResult
        [Route("/Result/Classwise")]
        [HttpPost]
        public async Task<ActionResult> PostResult([FromBody] CanLoadVM criteria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var selectedCandidate =await context.SelectedCandidates.Include("Candidate").Where(x => x.ExamId == criteria.ExamId && x.Candidate.StanderdClassId == criteria.StandardId).ToListAsync();
            foreach (var item in selectedCandidate)
            {
                item.Candidate.Exams = null;
                item.Candidate.SelectedExams = null;

            }
            
            return Ok(selectedCandidate);
        }
    }
}