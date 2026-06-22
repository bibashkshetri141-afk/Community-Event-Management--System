namespace CommunityEvents.Models
{
    public class Participant : Person
    {
        public string Phone { get; set; } = string.Empty;

        // Override polymorphism
        public override string GetContactInfo()
        {
            return $"{Name} ({Email}, {Phone})";
        }

        // Implement abstract method
        public override string GetRole() => "Participant";

        public List<Registration> Registrations { get; set; } = new();
    }
}