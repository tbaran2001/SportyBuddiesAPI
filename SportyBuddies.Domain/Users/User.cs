﻿using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Conversations;
using SportyBuddies.Domain.Sports;
using SportyBuddies.Domain.Users.Events;

namespace SportyBuddies.Domain.Users;

public class User : Entity
{
    private User(
        Guid id,
        string? name,
        Preferences preferences,
        DateOnly? dateOfBirth,
        Gender gender,
        string? description,
        DateTime createdOnUtc,
        UserPhoto? mainPhoto
    ) : base(id)
    {
        Name = name;
        Preferences = preferences;
        DateOfBirth = dateOfBirth;
        Gender = gender;
        Description = description;
        CreatedOnUtc = createdOnUtc;
        MainPhoto = mainPhoto;
        MainPhotoId = mainPhoto?.Id;
    }

    public string? Name { get; private set; }
    public string? Description { get; private set; }
    public DateTime CreatedOnUtc { get; private set; }
    public DateOnly? DateOfBirth { get; private set; }
    public Gender? Gender { get; private set; }
    public Guid? MainPhotoId { get; private set; }
    public UserPhoto? MainPhoto { get; private set; }
    public ICollection<UserPhoto> Photos { get; private set; } = new List<UserPhoto>();
    public ICollection<Sport> Sports { get; private set; } = new List<Sport>();
    public Preferences? Preferences { get; private set; }

    public ICollection<Message> Messages { get; set; }
    public ICollection<Conversation> Conversations { get; set; }

    public static User Create(Guid id, string name, DateOnly dateOfBirth, Gender gender)
    {
        return new User(
            id: id,
            name: name,
            preferences: Preferences.Default,
            dateOfBirth: dateOfBirth,
            gender: gender,
            description: null,
            createdOnUtc: DateTime.UtcNow,
            mainPhoto: null);
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
        AddDomainEvent(new UserDeletedEvent(Id));
    }

    public void AddSport(Sport sport)
    {
        if (Sports.Contains(sport))
            throw new Exception("User already has this sport");

        Sports.Add(sport);
        AddDomainEvent(new UserSportAddedEvent(Id, sport.Id));
    }

    public void RemoveSport(Sport sport)
    {
        if (!Sports.Contains(sport))
            throw new Exception("User does not have this sport");

        Sports.Remove(sport);
        AddDomainEvent(new UserSportRemovedEvent(Id, sport.Id));
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

    private User()
    {
    }
}