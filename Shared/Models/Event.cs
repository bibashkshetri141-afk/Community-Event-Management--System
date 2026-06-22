namespace CommunityEvents.Models
{
    public class Event : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Time { get; set; } = string.Empty;

        // Relationships
        public int VenueId { get; set; }
        public Venue? Venue { get; set; }

        public List<EventActivity> EventActivities { get; set; } = new();
        public List<Registration> Registrations { get; set; } = new();
    }
}