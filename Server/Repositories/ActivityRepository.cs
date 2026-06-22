using Microsoft.EntityFrameworkCore;
using Server.Data;
using CommunityEvents.Models;

namespace Server.Repositories
{
    public class ActivityRepository : IRepository<Activity>
    {
        private readonly AppDbContext _db;
        public ActivityRepository(AppDbContext db) => _db = db;

        public async Task<IEnumerable<Activity>> GetAllAsync() =>
            await _db.Activities.ToListAsync();

        public async Task<Activity?> GetByIdAsync(int id) =>
            await _db.Activities.FindAsync(id);

        public async Task<Activity> CreateAsync(Activity activity)
        {
            _db.Activities.Add(activity);
            await _db.SaveChangesAsync();
            return activity;
        }

        public async Task<Activity?> UpdateAsync(Activity activity)
        {
            var existing = await _db.Activities.FindAsync(activity.Id);
            if (existing == null) return null;
            existing.Name = activity.Name;
            existing.Type = activity.Type;
            existing.Description = activity.Description;
            await _db.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var activity = await _db.Activities.FindAsync(id);
            if (activity == null) return false;
            _db.Activities.Remove(activity);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}