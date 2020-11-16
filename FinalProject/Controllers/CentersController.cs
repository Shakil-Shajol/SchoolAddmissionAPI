using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProject.Models;
using FinalProject.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CentersController : ControllerBase
    {
        private readonly IAdmissionRepo<ExamCenter> _repo;

        public CentersController(IAdmissionRepo<ExamCenter> repo)
        {
            _repo = repo;
        }
        // GET: api/Centers
        [HttpGet]
        public async Task<IEnumerable<ExamCenter>> GetCenters()
        {
            return await _repo.GetT();
        }
        // GET: api/Centers/1
        [HttpGet("{id}")]
        public async Task<ActionResult> GetCenter([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var center = await _repo.GetTById(id);
            if (center == null)
            {
                return NotFound();
            }
            return Ok(center);
        }

        // POST: api/Centers
        [HttpPost]
        public async Task<ActionResult> PostCenter([FromBody] ExamCenter center)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _repo.Add(center);
            var save = await _repo.SaveAsync(center);
            return CreatedAtAction("GetCenter", new { id = center.CenterId }, center);
        }

        // PUT: api/Centers/1
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExamCenter([FromRoute] int id, [FromBody] ExamCenter center)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != center.CenterId)
            {
                return BadRequest();
            }
            try
            {
                _repo.Update(center);
                var save = await _repo.SaveAsync(center);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CenterExists(id))
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


        // DELETE: api/Centers/1
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteExamCenter([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var center = await _repo.GetTById(id);
            if (center == null)
            {
                return NotFound();
            }
            _repo.Delete(center);
            await _repo.SaveAsync(center);
            return Ok(center);
        }


        private bool CenterExists(int id)
        {
            var center = _repo.GetTById(id);
            if (center == null)
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