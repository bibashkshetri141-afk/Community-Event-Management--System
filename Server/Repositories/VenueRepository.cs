using Microsoft.EntityFrameworkCore;
using Server.Data;
using CommunityEvents.Models;

namespace Server.Repositories
{
    public class VenueRepository : IRepository<Venue>
    {
        private readonly AppDbContext _db;
        public VenueRepository(AppDbContext db) => _db = db;

        public async Task<IEnumerable<Venue>> GetAllAsync() =>
            await _db.Venues.ToListAsync();

        public async Task<Venue?> GetByIdAsync(int id) =>
            await _db.Venues.FindAsync(id);

        public async Task<Venue> CreateAsync(Venue venue)
        {
            _db.Venues.Add(venue);
            await _db.SaveChangesAsync();
            return venue;
        }

        public async Task<Venue?> UpdateAsync(Venue venue)
        {
            var existing = await _db.Venues.FindAsync(venue.Id);
            if (existing == null) return null;
            existing.Name = venue.Name;
            existing.Address = venue.Address;
            existing.Capacity = venue.Capacity;
            await _db.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var venue = await _db.Venues.FindAsync(id);
            if (venue == null) return false;
            _db.Venues.Remove(venue);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}