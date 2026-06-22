using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using CommunityEvents.Models;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationsController : ControllerBase
    {
        private readonly AppDbContext _db;
        public RegistrationsController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var registrations = await _db.Registrations
                .Include(r => r.Event)
                .Include(r => r.Participant)
                .ToListAsync();
            return Ok(registrations);
        }

        [HttpGet("participant/{participantId}")]
        public async Task<IActionResult> GetByParticipant(int participantId)
        {
            var registrations = await _db.Registrations
                .Include(r => r.Event)
                   .ThenInclude(e => e!.Venue)
                .Where(r => r.ParticipantId == participantId)
                .ToListAsync();
            return Ok(registrations);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Registration registration)
        {
            registration.RegistrationDate = DateTime.Now;
            _db.Registrations.Add(registration);
            await _db.SaveChangesAsync();
            return Ok(registration);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var registration = await _db.Registrations.FindAsync(id);
            if (registration == null) return NotFound();
            _db.Registrations.Remove(registration);
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}