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
    public class RoomsController : ControllerBase
    {
        private readonly IAdmissionRepo<Room> _repo;
        private readonly IAdmissionRepo<ExamCenter> _centerRepo;

        public RoomsController(IAdmissionRepo<Room> repo, IAdmissionRepo<ExamCenter> centerRepo)
        {
            _repo = repo;
            _centerRepo = centerRepo;
        }
        public async Task<IEnumerable<Room>> GetRooms()
        {
            //return await _repo.GetT();
            var rooms= await _repo.GetT();
            foreach (var room in rooms)
            {
                room.ExamCenter = await _centerRepo.GetTById(room.CenterId);
                room.ExamCenter.Rooms = null;
            }
            return rooms;
        }
        // GET: api/Rooms/1
        [HttpGet("{id}")]
        public async Task<ActionResult> GetRoom([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var room = await _repo.GetTById(id);
            if (room == null)
            {
                return NotFound();
            }
            room.ExamCenter =await _centerRepo.GetTById(room.CenterId);
            room.ExamCenter.Rooms = null;
            return Ok(room);
        }

        // POST: api/Rooms
        [HttpPost]
        public async Task<ActionResult> PostRoom([FromBody] Room room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _repo.Add(room);
            var save = await _repo.SaveAsync(room);
            return CreatedAtAction("GetRoom", new { id = room.RoomId }, room);
        }

        // PUT: api/Rooms/1
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom([FromRoute] int id, [FromBody] Room room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != room.RoomId)
            {
                return BadRequest();
            }
            try
            {
                _repo.Update(room);
                var save = await _repo.SaveAsync(room);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomExists(id))
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


        // DELETE: api/Rooms/1
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRoom([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var room = await _repo.GetTById(id);
            if (room == null)
            {
                return NotFound();
            }
            _repo.Delete(room);
            await _repo.SaveAsync(room);
            return Ok(room);
        }


        private bool RoomExists(int id)
        {
            var room = _repo.GetTById(id);
            if (room == null)
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