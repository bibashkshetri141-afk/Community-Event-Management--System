using Microsoft.EntityFrameworkCore;
using Server.Data;
using CommunityEvents.Models;

namespace Server.Repositories
{
    public class ParticipantRepository : IRepository<Participant>
    {
        private readonly AppDbContext _db;
        public ParticipantRepository(AppDbContext db) => _db = db;

        public async Task<IEnumerable<Participant>> GetAllAsync() =>
            await _db.Participants.ToListAsync();

        public async Task<Participant?> GetByIdAsync(int id) =>
            await _db.Participants.FindAsync(id);

        public async Task<Participant> CreateAsync(Participant participant)
        {
            _db.Participants.Add(participant);
            await _db.SaveChangesAsync();
            return participant;
        }

        public async Task<Participant?> UpdateAsync(Participant participant)
        {
            var existing = await _db.Participants.FindAsync(participant.Id);
            if (existing == null) return null;
            existing.Name = participant.Name;
            existing.Email = participant.Email;
            existing.Phone = participant.Phone;
            await _db.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var participant = await _db.Participants.FindAsync(id);
            if (participant == null) return false;
            _db.Participants.Remove(participant);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}