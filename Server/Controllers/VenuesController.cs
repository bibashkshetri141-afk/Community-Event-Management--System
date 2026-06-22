using Microsoft.AspNetCore.Mvc;
using Server.Exceptions;
using Server.Repositories;
using CommunityEvents.Models;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VenuesController : ControllerBase
    {
        private readonly IRepository<Venue> _repo;

        public VenuesController(IRepository<Venue> repo) => _repo = repo;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var venues = await _repo.GetAllAsync();
                return Ok(venues);
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
                var venue = await _repo.GetByIdAsync(id)
                    ?? throw new NotFoundException("Venue", id);
                return Ok(venue);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(Venue venue)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(venue.Name))
                    throw new Exceptions.ValidationException("Venue name is required.");
                if (venue.Capacity <= 0)
                    throw new Exceptions.ValidationException("Capacity must be greater than zero.");

                var created = await _repo.CreateAsync(venue);
                return Ok(created);
            }
            catch (Exceptions.ValidationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Venue venue)
        {
            try
            {
                if (id != venue.Id)
                    throw new Exceptions.ValidationException("ID mismatch.");

                var updated = await _repo.UpdateAsync(venue)
                    ?? throw new NotFoundException("Venue", id);
                return Ok(updated);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exceptions.ValidationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _repo.DeleteAsync(id);
                if (!deleted) throw new NotFoundException("Venue", id);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}