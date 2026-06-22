namespace CommunityEvents.Models
{
    public abstract class Person : IEntity, IContactable
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public virtual string GetContactInfo()
        {
            return $"{Name} ({Email})";
        }

        public abstract string GetRole();
    }
}