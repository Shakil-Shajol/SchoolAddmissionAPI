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
    public class SessionsController : ControllerBase
    {
        private readonly IAdmissionRepo<Session> _repo;

        public SessionsController(IAdmissionRepo<Session> repo)
        {
            _repo = repo;
        }
        // GET: api/Sessions
        [HttpGet]
        public async Task<IEnumerable<Session>> Get()
        {
            return await _repo.GetT();
        }

        // GET: api/Sessions/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var session= await _repo.GetTById(id);
            if (session == null)
            {
                return NotFound();
            }
            return Ok(session);

        }

        // POST: api/Sessions
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Session session)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _repo.Add(session);
            await _repo.SaveAsync(session);
            return CreatedAtAction("GetById", new { id = session.SessionId }, session);
        }

        // PUT: api/Sessions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] Session session)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != session.SessionId)
            {
                return BadRequest();
            }
            try
            {
                _repo.Update(session);
                var save = await _repo.SaveAsync(session);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SessionExists(id))
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

        private bool SessionExists(int id)
        {
            var session = _repo.GetTById(id);
            if (session == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var session = await _repo.GetTById(id);
            if (session == null)
            {
                return NotFound();
            }
            _repo.Delete(session);
            await _repo.SaveAsync(session);
            return Ok(session);
        }
    }
}
