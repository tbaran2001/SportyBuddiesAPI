using AutoMapper;
using FluentAssertions;
using NSubstitute;
using SportyBuddies.Application.Common.DTOs.Profile;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Application.Features.Profiles.Queries.GetProfile;
using SportyBuddies.Application.Mappings;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using SportyBuddies.Domain.Common.Interfaces.Services;
using Profile = SportyBuddies.Domain.Profiles.Profile;

namespace SportyBuddies.Application.UnitTests.Profiles.Queries;

public class GetProfileTests
{
    private readonly GetProfileQuery _query= new(Guid.NewGuid());
    private readonly GetProfileQueryHandler _handler;
    private readonly IProfilesRepository _profilesRepositoryMock;
    private readonly IBlobStorageService _blobStorageServiceMock;

    public GetProfileTests()
    {
        var configurationProvider = new MapperConfiguration(cfg => { cfg.AddProfile<ProfileMappingProfile>(); });
        var mapper = configurationProvider.CreateMapper();
        _blobStorageServiceMock = Substitute.For<IBlobStorageService>();
    
        _profilesRepositoryMock = Substitute.For<IProfilesRepository>();
        _handler = new GetProfileQueryHandler(_profilesRepositoryMock, mapper, _blobStorageServiceMock);
    }

    [Fact]
    public async Task Handle_ShouldReturnUser_WhenUserExists()
    {
        // Arrange
        var user = Profile.Create(_query.ProfileId);
        _profilesRepositoryMock.GetProfileByIdWithSportsAsync(_query.ProfileId).Returns(user);

        // Act
        var result = await _handler.Handle(_query, default);

        // Assert
        result.Should().BeOfType<ProfileWithSportsResponse>();
        result.Id.Should().Be(user.Id);
    }
    
    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenUserDoesNotExist()
    {
        // Arrange
        _profilesRepositoryMock.GetProfileByIdWithSportsAsync(_query.ProfileId).Returns((Profile?)null);

        // Act
        var act = async () => await _handler.Handle(_query, default);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}