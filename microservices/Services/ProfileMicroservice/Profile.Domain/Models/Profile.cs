using Profile.Domain.Common;
using Profile.Domain.Events;
using Profile.Domain.Exceptions;
using Profile.Domain.ValueObjects;

namespace Profile.Domain.Models;

public class Profile : Entity
{
    public ProfileName Name { get; private set; } = default!;
    public ProfileDescription Description { get; private set; } = default!;
    public DateTime CreatedOnUtc { get; private set; }
    public DateOnly DateOfBirth { get; private set; }
    public Gender Gender { get; private set; }
    public string? MainPhotoUrl { get; private set; }
    public Preferences? Preferences { get; private set; }

    private readonly List<Sport> _sports = new();
    public IReadOnlyList<Sport> Sports => _sports.AsReadOnly();

    public static Profile Create(
        Guid id,
        ProfileName name,
        ProfileDescription description,
        DateTime createdOnUtc,
        DateOnly dateOfBirth,
        Gender gender,
        Preferences preferences)
    {
        var profile = new Profile
        {
            Id = id,
            Name = name,
            Description = description,
            CreatedOnUtc = createdOnUtc,
            DateOfBirth = dateOfBirth,
            Gender = gender,
            Preferences = preferences,
        };

        profile.AddDomainEvent(new ProfileCreatedEvent(profile));

        return profile;
    }

    public void Update(
        ProfileName name,
        ProfileDescription description,
        DateOnly dateOfBirth,
        Gender gender)
    {
        Name = name;
        Description = description;
        DateOfBirth = dateOfBirth;
        Gender = gender;

        AddDomainEvent(new ProfileUpdatedEvent(this));
    }

    public void AddSport(Sport sport)
    {
        if(_sports.Contains(sport))
            throw new DomainException("Profile already has this sport.");

        _sports.Add(sport);
        AddDomainEvent(new ProfileSportAddedEvent(this));
    }

    public void RemoveSport(Sport sport)
    {
        if(!_sports.Contains(sport))
            throw new DomainException("Profile does not have this sport.");

        _sports.Remove(sport);
        AddDomainEvent(new ProfileSportRemovedEvent(this));
    }
}