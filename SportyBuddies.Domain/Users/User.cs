using ErrorOr;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Sports;
using SportyBuddies.Domain.Users.Events;

namespace SportyBuddies.Domain.Users;

public class User : Entity
{
    public User(string name, string? description, DateTime lastActive, ICollection<Sport> sports, UserPhoto? mainPhoto,
        ICollection<UserPhoto> photos, DateTime? dateOfBirth, Gender? gender = 0, Guid? id = null) :
        base(id ?? Guid.NewGuid())
    {
        Name = name;
        Description = description;
        Sports = sports;
        LastActive = lastActive;
        MainPhoto = mainPhoto;
        Photos = photos;
        MainPhotoId = mainPhoto?.Id;
        Gender = gender;
        DateOfBirth = dateOfBirth;
    }

    public User()
    {
    }

    public string Name { get; private set; }
    public string? Description { get; private set; }
    public DateTime LastActive { get; private set; } = DateTime.Now;
    public ICollection<Sport> Sports { get; private set; } = new List<Sport>();
    public UserPhoto? MainPhoto { get; private set; }
    public Guid? MainPhotoId { get; private set; }
    public ICollection<UserPhoto> Photos { get; private set; } = new List<UserPhoto>();
    public Gender? Gender { get; private set; }
    public DateTime? DateOfBirth { get; private set; }
    public Preferences? Preferences { get; private set; }

    public ErrorOr<Success> UpdatePreferences(Preferences preferences)
    {
        Preferences = preferences;
        return Result.Success;
    }

    public void Delete()
    {
        AddDomainEvent(new UserDeletedEvent(Id));
    }

    public ErrorOr<Success> AddSport(Sport sport)
    {
        if (Sports.Contains(sport))
            return Error.Conflict(description: "User already has this sport");

        Sports.Add(sport);
        return Result.Success;
    }

    public ErrorOr<Success> RemoveSport(Sport sport)
    {
        if (!Sports.Contains(sport))
            return Error.NotFound(description: "User does not have this sport");

        Sports.Remove(sport);
        return Result.Success;
    }

    public void RemoveAllSports()
    {
        Sports.Clear();
    }

    public void AddPhoto(UserPhoto photo)
    {
        if (photo.IsMain)
        {
            MainPhoto = photo;
            MainPhotoId = photo.Id;
        }
        else
        {
            Photos.Add(photo);
        }
    }
}