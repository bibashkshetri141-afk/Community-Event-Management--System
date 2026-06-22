using Microsoft.EntityFrameworkCore;
using CommunityEvents.Models;

namespace Server.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Event> Events { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<EventActivity> EventActivities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Set up the composite key for EventActivity (junction table)
            modelBuilder.Entity<EventActivity>()
                .HasKey(ea => new { ea.EventId, ea.ActivityId });

            // Event → Venue relationship
            modelBuilder.Entity<Event>()
                .HasOne(e => e.Venue)
                .WithMany(v => v.Events)
                .HasForeignKey(e => e.VenueId);

            // Registration → Event relationship
            modelBuilder.Entity<Registration>()
                .HasOne(r => r.Event)
                .WithMany(e => e.Registrations)
                .HasForeignKey(r => r.EventId);

            // Registration → Participant relationship
            modelBuilder.Entity<Registration>()
                .HasOne(r => r.Participant)
                .WithMany(p => p.Registrations)
                .HasForeignKey(r => r.ParticipantId);

            // EventActivity → Event relationship
            modelBuilder.Entity<EventActivity>()
                .HasOne(ea => ea.Event)
                .WithMany(e => e.EventActivities)
                .HasForeignKey(ea => ea.EventId);

            // EventActivity → Activity relationship
            modelBuilder.Entity<EventActivity>()
                .HasOne(ea => ea.Activity)
                .WithMany(a => a.EventActivities)
                .HasForeignKey(ea => ea.ActivityId);
        }
    }
}