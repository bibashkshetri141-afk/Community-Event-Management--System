using Microsoft.EntityFrameworkCore;
using Server.Data;
using CommunityEvents.Models;

namespace Server.Repositories
{
    public class EventRepository : IRepository<Event>
    {
        private readonly AppDbContext _db;
        public EventRepository(AppDbContext db) => _db = db;

        public async Task<IEnumerable<Event>> GetAllAsync() =>
            await _db.Events
                .Include(e => e.Venue)
                .Include(e => e.EventActivities)
                    .ThenInclude(ea => ea.Activity)
                .Include(e => e.Registrations)
                .ToListAsync();

        public async Task<Event?> GetByIdAsync(int id) =>
            await _db.Events
                .Include(e => e.Venue)
                .Include(e => e.EventActivities)
                    .ThenInclude(ea => ea.Activity)
                .FirstOrDefaultAsync(e => e.Id == id);

        public async Task<Event> CreateAsync(Event ev)
        {
            _db.Events.Add(ev);
            await _db.SaveChangesAsync();
            return ev;
        }

        public async Task<Event?> UpdateAsync(Event ev)
        {
            var existing = await _db.Events
                .Include(e => e.EventActivities)
                .FirstOrDefaultAsync(e => e.Id == ev.Id);

            if (existing == null) return null;

            existing.Name = ev.Name;
            existing.Description = ev.Description;
            existing.Date = ev.Date;
            existing.Time = ev.Time;
            existing.VenueId = ev.VenueId;

            // Update activities
            existing.EventActivities.Clear();
            if (ev.EventActivities != null)
            {
                foreach (var ea in ev.EventActivities)
                {
                    existing.EventActivities.Add(new EventActivity
                    {
                        EventId = existing.Id,
                        ActivityId = ea.ActivityId
                    });
                }
            }

            await _db.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var ev = await _db.Events.FindAsync(id);
            if (ev == null) return false;
            _db.Events.Remove(ev);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}