namespace CommunityEvents.Models
{
    public class Admin : Person
    {
        public string Username { get; set; } = string.Empty;

        public override string GetContactInfo()
        {
            return $"Admin: {Name} ({Email})";
        }

        public override string GetRole() => "Administrator";
    }
}