using Microsoft.AspNetCore.Mvc;
using Server.Exceptions;
using Server.Repositories;
using CommunityEvents.Models;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParticipantsController : ControllerBase
    {
        private readonly IRepository<Participant> _repo;
        public ParticipantsController(IRepository<Participant> repo) => _repo = repo;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _repo.GetAllAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var participant = await _repo.GetByIdAsync(id)
                    ?? throw new NotFoundException("Participant", id);
                return Ok(participant);
            }
            catch (NotFoundException ex) { return NotFound(new { error = ex.Message }); }
            catch (Exception ex) { return StatusCode(500, new { error = ex.Message }); }
        }

        [HttpPost]
        public async Task<IActionResult> Create(Participant participant)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(participant.Name))
                    throw new ValidationException("Name is required.");
                if (string.IsNullOrWhiteSpace(participant.Email))
                    throw new ValidationException("Email is required.");
                return Ok(await _repo.CreateAsync(participant));
            }
            catch (ValidationException ex) { return BadRequest(new { error = ex.Message }); }
            catch (Exception ex) { return StatusCode(500, new { error = ex.Message }); }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Participant participant)
        {
            try
            {
                if (id != participant.Id)
                    throw new ValidationException("ID mismatch.");
                var updated = await _repo.UpdateAsync(participant)
                    ?? throw new NotFoundException("Participant", id);
                return Ok(updated);
            }
            catch (NotFoundException ex) { return NotFound(new { error = ex.Message }); }
            catch (ValidationException ex) { return BadRequest(new { error = ex.Message }); }
            catch (Exception ex) { return StatusCode(500, new { error = ex.Message }); }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (!await _repo.DeleteAsync(id))
                    throw new NotFoundException("Participant", id);
                return Ok();
            }
            catch (NotFoundException ex) { return NotFound(new { error = ex.Message }); }
            catch (Exception ex) { return StatusCode(500, new { error = ex.Message }); }
        }
    }
}