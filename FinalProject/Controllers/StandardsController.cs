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
    public class StandardsController : ControllerBase
    {
        private readonly IAdmissionRepo<StanderdClass> _repo;

        public StandardsController(IAdmissionRepo<StanderdClass> repo)
        {
            _repo = repo;
        }

        // GET: api/Standards
        [HttpGet]
        public async Task<IEnumerable<StanderdClass>> GetStandards()
        {
            return await _repo.GetT();
        }

        // GET: api/Standards/1
        [HttpGet("{id}")]
        public async Task<ActionResult> GetStandard([FromRoute] int id)
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
            return Ok(standard);
        }

        // POST: api/Standards
        [HttpPost]
        public async Task<ActionResult> PostStandard([FromBody] StanderdClass standard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _repo.Add(standard);
            var save = await _repo.SaveAsync(standard);
            return CreatedAtAction("GetStandard", new { id = standard.ClassId }, standard);
        }

        // PUT: api/Standards/1
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStandard([FromRoute] int id, [FromBody] StanderdClass standard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != standard.ClassId)
            {
                return BadRequest();
            }
            try
            {
                _repo.Update(standard);
                var save = await _repo.SaveAsync(standard);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StandardExists(id))
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


        // DELETE: api/Standards/1
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStandard([FromRoute] int id)
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


        private bool StandardExists(int id)
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