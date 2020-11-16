using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FinalProject.Models;
using FinalProject.Repository;
using FinalProject.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatesController : ControllerBase
    {
        private readonly IAdmissionRepo<Candidate> _repo;
        private readonly IAdmissionRepo<RoomCandidate> _seat;
        private readonly IMapper _mapper;
        private readonly IAdmissionRepo<Room> _room;
        private readonly IResultRepo _roomCandidateRepo;
        private readonly IHostingEnvironment _hostEnv;

        public CandidatesController(IAdmissionRepo<Candidate> repo, IAdmissionRepo<RoomCandidate> seat,IMapper mapper, IAdmissionRepo<Room> room,IResultRepo roomCandidateRepo, IHostingEnvironment hosting)
        {
            _repo = repo;
            _seat = seat;
            _mapper = mapper;
            _room = room;
            _roomCandidateRepo = roomCandidateRepo;
            _hostEnv = hosting;
        }
        // POST: api/Candidates
        [HttpPost]
        public async Task<ActionResult> PostCandidate([FromForm]CandidateAddViewModel candidate)
        {
            var a = candidate;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (candidate.ExamId>0)
            {
                if (candidate.TC!=null && candidate.Image!=null)
                {
                    string NewImageName = Guid.NewGuid().ToString() + "_" + candidate.Image.FileName;
                    string NewImagePath = Path.Combine("Images", NewImageName);
                    string image = Path.Combine(_hostEnv.WebRootPath, NewImagePath);
                    candidate.Image.CopyTo(new FileStream(image, FileMode.Create));



                    string NewFileName = Guid.NewGuid().ToString() + "_" + candidate.TC.FileName;
                    string NewFilePath = Path.Combine("Files", NewFileName);
                    string file = Path.Combine(_hostEnv.WebRootPath, NewFilePath);
                    candidate.Image.CopyTo(new FileStream(file, FileMode.Create));


                    Candidate candidateToPost = _mapper.Map<Candidate>(candidate);
                    candidateToPost.ImagePath = NewImagePath;
                    candidateToPost.TCPath = NewFilePath;
                    _repo.Add(candidateToPost);
                    var save = await _repo.SaveAsync(candidateToPost);
                    var rooms = await _room.GetT();
                    foreach (var room in rooms)
                    {
                        if (_roomCandidateRepo.CountCandidatesInRoom(room.RoomId, candidate.ExamId) < room.Capacity)
                        {
                            RoomCandidate rc = new RoomCandidate { RoomId = room.RoomId, ExamId = candidate.ExamId, CandidateId = candidateToPost.CandidateId };
                            _seat.Add(rc);
                            await _seat.SaveAsync(rc);
                            candidateToPost.Exams = null;
                            rc.Room.RoomCandidates = null;
                            return Ok(rc);
                        }
                    }
                    return Ok(candidateToPost);
                }

            }
            return BadRequest();

        }
        // GET: api/Candidates
        [HttpGet]
        public async Task<IEnumerable<Candidate>> GetCandidates()
        {
            return await _repo.GetT();
        }

        // GET: api/Candidates/1
        [HttpGet("{id}")]
        public async Task<ActionResult> GetCandidate([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var candidate = await _repo.GetTById(id);
            if (candidate == null)
            {
                return NotFound();
            }
            return Ok(candidate);
        }



        // PUT: api/Candidates/1
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCandidate([FromRoute] int id, [FromBody] Candidate candidate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != candidate.CandidateId)
            {
                return BadRequest();
            }
            try
            {
                _repo.Update(candidate);
                var save = await _repo.SaveAsync(candidate);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CandidateExists(id))
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


        // DELETE: api/Candidates/1
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCandidate([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var candidate = await _repo.GetTById(id);
            if (candidate == null)
            {
                return NotFound();
            }
            _repo.Delete(candidate);
            await _repo.SaveAsync(candidate);
            return Ok(candidate);
        }


        private bool CandidateExists(int id)
        {
            var candidate = _repo.GetTById(id);
            if (candidate == null)
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