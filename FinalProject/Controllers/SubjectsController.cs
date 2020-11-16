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
    public class SubjectsController : ControllerBase
    {
        private readonly IAdmissionRepo<Subject> _repo;

        public SubjectsController(IAdmissionRepo<Subject> repo)
        {
            _repo = repo;
        }

        // GET: api/Subjects
        [HttpGet]
        public async Task<IEnumerable<Subject>> GetSubjects()
        {
            return await _repo.GetT();
        }

        // GET: api/Subjects/1
        [HttpGet("{id}")]
        public async Task<ActionResult> GetSubject([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var subject = await _repo.GetTById(id);
            if (subject == null)
            {
                return NotFound();
            }
            return Ok(subject);
        }

        // POST: api/Subjects
        [HttpPost]
        public async Task<ActionResult> PostSubject([FromBody] Subject subject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _repo.Add(subject);
            var save = await _repo.SaveAsync(subject);
            return CreatedAtAction("GetSubjects", new { id = subject.SubjectId }, subject);
        }

        // PUT: api/Subjects/1
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubject([FromRoute] int id, [FromBody] Subject subject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != subject.SubjectId)
            {
                return BadRequest();
            }
            try
            {
                _repo.Update(subject);
                var save = await _repo.SaveAsync(subject);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjectExists(id))
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


        // DELETE: api/Subjects/1
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSubject([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var subject = await _repo.GetTById(id);
            if (subject == null)
            {
                return NotFound();
            }
            _repo.Delete(subject);
            await _repo.SaveAsync(subject);
            return Ok(subject);
        }


        private bool SubjectExists(int id)
        {
            var subject = _repo.GetTById(id);
            if (subject == null)
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