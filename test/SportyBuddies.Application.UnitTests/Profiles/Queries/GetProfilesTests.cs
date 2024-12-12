using AutoMapper;
using FluentAssertions;
using NSubstitute;
using SportyBuddies.Application.Features.Profiles.Queries.GetProfiles;
using SportyBuddies.Application.Mappings;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using Profile = SportyBuddies.Domain.Profiles.Profile;

namespace SportyBuddies.Application.UnitTests.Profiles.Queries;

public class GetProfilesTests
{
    private readonly GetProfilesQuery _query= new();
    private readonly GetProfilesQueryHandler _handler;
    private readonly IProfilesRepository _profilesRepositoryMock;

    public GetProfilesTests()
    {
        var configurationProvider = new MapperConfiguration(cfg => { cfg.AddProfile<ProfileMappingProfile>(); });
        var mapper = configurationProvider.CreateMapper();
    
        _profilesRepositoryMock = Substitute.For<IProfilesRepository>();
        _handler = new GetProfilesQueryHandler(_profilesRepositoryMock, mapper);
    }

    [Fact]
    public async Task Handle_Should_ReturnUsers()
    {
        // Arrange
        var users = new List<Profile>
        {
            Profile.Create(Guid.NewGuid()),
            Profile.Create(Guid.NewGuid())
        };
        _profilesRepositoryMock.GetAllProfilesAsync().Returns(users);
        
        // Act
        var result = await _handler.Handle(_query, default);
        
        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(users.Count);
    }

}