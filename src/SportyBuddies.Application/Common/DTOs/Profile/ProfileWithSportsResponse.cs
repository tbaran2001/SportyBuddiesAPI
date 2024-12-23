﻿using SportyBuddies.Application.Common.DTOs.Sport;
using SportyBuddies.Domain.Profiles;

namespace SportyBuddies.Application.Common.DTOs.Profile;

public class ProfileWithSportsResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public string? MainPhotoSasUrl { get; set; }
    public Gender Gender { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public Preferences Preferences { get; set; }
    public ICollection<SportResponse> Sports { get; set; }
}