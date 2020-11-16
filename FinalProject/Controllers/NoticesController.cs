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
    public class NoticesController : ControllerBase
    {
        private readonly IAdmissionRepo<Notice> _repo;
        private readonly IResultRepo _nRepo;

        public NoticesController(IAdmissionRepo<Notice> repo,IResultRepo nRepo)
        {
            _repo = repo;
            _nRepo = nRepo;
        }

        // GET: api/Notices
        [HttpGet]
        public async Task<IEnumerable<Notice>> GetNotices()
        {
            return await _repo.GetT();
        }
        [Route("/LatestNotice")]
        [HttpGet]
        public async Task<Notice> GetLatestNotices()
        {
            return await _nRepo.LatestNotice();
        }

        // GET: api/Notices/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Notice>> GetNotice([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var notice = await _repo.GetTById(id);
            if (notice == null)
            {
                return NotFound();
            }
            return notice;
        }

        // POST: api/Notices
        [HttpPost]
        public async Task<ActionResult> PostNotice([FromBody] Notice notice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _repo.Add(notice);
            var save = await _repo.SaveAsync(notice);
            return CreatedAtAction("GetNotices", new { id = notice.NoticeID }, notice);
        }

        // PUT: api/Notices/1
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNotice([FromRoute] int id, [FromBody] Notice notice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != notice.NoticeID)
            {
                return BadRequest();
            }
            try
            {
                _repo.Update(notice);
                var save = await _repo.SaveAsync(notice);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoticeExists(id))
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


        // DELETE: api/Notices/1
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteNotice([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var notice = await _repo.GetTById(id);
            if (notice == null)
            {
                return NotFound();
            }
            _repo.Delete(notice);
            await _repo.SaveAsync(notice);
            return Ok(notice);
        }


        private bool NoticeExists(int id)
        {
            var notice = _repo.GetTById(id);
            if (notice==null)
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