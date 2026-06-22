namespace CommunityEvents.Models
{
    public class Registration : IEntity
    {
        public int Id { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Status { get; set; } = "Pending";

        public int EventId { get; set; }
        public Event? Event { get; set; }

        public int ParticipantId { get; set; }
        public Participant? Participant { get; set; }
    }
}