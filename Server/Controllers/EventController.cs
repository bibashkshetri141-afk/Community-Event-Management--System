using Microsoft.AspNetCore.Mvc;
using Server.Exceptions;
using Server.Repositories;
using CommunityEvents.Models;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IRepository<Event> _repo;
        public EventsController(IRepository<Event> repo) => _repo = repo;

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
                var ev = await _repo.GetByIdAsync(id)
                    ?? throw new NotFoundException("Event", id);
                return Ok(ev);
            }
            catch (NotFoundException ex) { return NotFound(new { error = ex.Message }); }
            catch (Exception ex) { return StatusCode(500, new { error = ex.Message }); }
        }

        [HttpPost]
        public async Task<IActionResult> Create(Event ev)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(ev.Name))
                    throw new ValidationException("Event name is required.");
                if (ev.VenueId == 0)
                    throw new ValidationException("A venue must be selected.");
                return Ok(await _repo.CreateAsync(ev));
            }
            catch (ValidationException ex) { return BadRequest(new { error = ex.Message }); }
            catch (Exception ex) { return StatusCode(500, new { error = ex.Message }); }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Event ev)
        {
            try
            {
                if (id != ev.Id)
                    throw new ValidationException("ID mismatch.");
                var updated = await _repo.UpdateAsync(ev)
                    ?? throw new NotFoundException("Event", id);
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
                    throw new NotFoundException("Event", id);
                return Ok();
            }
            catch (NotFoundException ex) { return NotFound(new { error = ex.Message }); }
            catch (Exception ex) { return StatusCode(500, new { error = ex.Message }); }
        }
    }
}