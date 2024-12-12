using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Conversations;
using SportyBuddies.Domain.Profiles.Events;
using SportyBuddies.Domain.Sports;

namespace SportyBuddies.Domain.Profiles;

public class Profile : Entity
{
    private Profile(
        Guid id,
        string? name,
        string? description,
        DateTime createdOnUtc,
        DateOnly? dateOfBirth,
        Gender gender,
        Preferences preferences
    ) : base(id)
    {
        Name = name;
        Preferences = preferences;
        DateOfBirth = dateOfBirth;
        Gender = gender;
        Description = description;
        CreatedOnUtc = createdOnUtc;
    }

    public string? Name { get; private set; }
    public string? Description { get; private set; }
    public DateTime CreatedOnUtc { get; private set; }
    public DateOnly? DateOfBirth { get; private set; }
    public Gender? Gender { get; private set; }
    public string? MainPhotoUrl { get; private set; }
    public Preferences? Preferences { get; private set; }
    public ICollection<Sport> Sports { get; private set; }= new List<Sport>();

    public ICollection<Message>? Messages { get; private set; }
    public ICollection<Conversation>? Conversations { get; private set; }

    public static Profile Create(Guid id, string name, DateOnly dateOfBirth, Gender gender)
    {
        return new Profile(
            id: id,
            name: name,
            preferences: Preferences.Default,
            dateOfBirth: dateOfBirth,
            gender: gender,
            description: null,
            createdOnUtc: DateTime.UtcNow);
    }

    public static Profile Create(Guid id)
    {
        return new Profile(
            id: id,
            name: null,
            preferences: Preferences.Default,
            dateOfBirth: null,
            gender: 0,
            description: null,
            createdOnUtc: DateTime.UtcNow);
    }

    public void Update(string name, string description, DateOnly dateOfBirth, Gender gender)
    {
        Name = name;
        Description = description;
        DateOfBirth = dateOfBirth;
        Gender = gender;
    }

    public void UpdatePreferences(Preferences preferences)
    {
        Preferences = preferences;
    }

    public void Delete()
    {
        AddDomainEvent(new ProfileDeletedEvent(Id));
    }

    public void AddSport(Sport sport)
    {
        if (Sports.Contains(sport))
            throw new Exception("Profile already has this sport");

        Sports.Add(sport);
        AddDomainEvent(new ProfileSportAddedEvent(Id, sport.Id));
    }

    public void RemoveSport(Sport sport)
    {
        if (!Sports.Contains(sport))
            throw new Exception("Profile does not have this sport");

        Sports.Remove(sport);
        AddDomainEvent(new ProfileSportRemovedEvent(Id, sport.Id));
    }

    public void RemoveAllSports()
    {
        Sports.Clear();
    }

    public void AddMainPhoto(string url)
    {
        MainPhotoUrl = url;
    }

    public void RemoveMainPhoto()
    {
        MainPhotoUrl = null;
    }

    private Profile()
    {
    }
}