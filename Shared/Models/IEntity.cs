namespace CommunityEvents.Models
{
    public interface IEntity
    {
        int Id { get; set; }
    }


    public interface IContactable
    {
        string GetContactInfo();
        string GetRole();
    }
}