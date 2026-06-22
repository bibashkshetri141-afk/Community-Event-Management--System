using Microsoft.AspNetCore.Mvc;
using Server.Exceptions;
using Server.Repositories;
using CommunityEvents.Models;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActivitiesController : ControllerBase
    {
        private readonly IRepository<Activity> _repo;
        public ActivitiesController(IRepository<Activity> repo) => _repo = repo;

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
                var activity = await _repo.GetByIdAsync(id)
                    ?? throw new NotFoundException("Activity", id);
                return Ok(activity);
            }
            catch (NotFoundException ex) { return NotFound(new { error = ex.Message }); }
            catch (Exception ex) { return StatusCode(500, new { error = ex.Message }); }
        }

        [HttpPost]
        public async Task<IActionResult> Create(Activity activity)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(activity.Name))
                    throw new ValidationException("Activity name is required.");
                return Ok(await _repo.CreateAsync(activity));
            }
            catch (ValidationException ex) { return BadRequest(new { error = ex.Message }); }
            catch (Exception ex) { return StatusCode(500, new { error = ex.Message }); }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Activity activity)
        {
            try
            {
                if (id != activity.Id)
                    throw new ValidationException("ID mismatch.");
                var updated = await _repo.UpdateAsync(activity)
                    ?? throw new NotFoundException("Activity", id);
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
                    throw new NotFoundException("Activity", id);
                return Ok();
            }
            catch (NotFoundException ex) { return NotFound(new { error = ex.Message }); }
            catch (Exception ex) { return StatusCode(500, new { error = ex.Message }); }
        }
    }
}