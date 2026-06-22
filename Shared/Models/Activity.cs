namespace CommunityEvents.Models
{
    public class Activity : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public List<EventActivity> EventActivities { get; set; } = new();
    }
}