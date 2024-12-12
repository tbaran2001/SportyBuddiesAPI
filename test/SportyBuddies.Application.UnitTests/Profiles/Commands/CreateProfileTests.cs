using AutoMapper;
using FluentAssertions;
using NSubstitute;
using SportyBuddies.Application.Common.DTOs.Profile;
using SportyBuddies.Application.Features.Profiles.Commands.CreateProfile;
using SportyBuddies.Application.Mappings;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using Profile = SportyBuddies.Domain.Profiles.Profile;

namespace SportyBuddies.Application.UnitTests.Profiles.Commands;

public class CreateProfileTests
{
    private readonly CreateProfileCommand _command = new("username", "description");
    private readonly CreateProfileCommandHandler _handler;
    private readonly IProfilesRepository _profilesRepositoryMock;
    private readonly IUnitOfWork _unitOfWorkMock;


    public CreateProfileTests()
    {
        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ProfileMappingProfile>();
            cfg.AddProfile<SportMappingProfile>();
        });
        var mapper = configurationProvider.CreateMapper();

        _profilesRepositoryMock = Substitute.For<IProfilesRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();
        _handler = new CreateProfileCommandHandler(_profilesRepositoryMock, mapper, _unitOfWorkMock);
    }

    [Fact]
    public async Task Handle_ShouldCreateUser_WhenValidRequest()
    {
        // Act
        var result = await _handler.Handle(_command, default);

        // Assert
        result.Should().BeOfType<ProfileWithSportsResponse>();
        result.Name.Should().Be(_command.Name);
        result.Description.Should().Be(_command.Description);

        await _profilesRepositoryMock.Received(1).AddProfileAsync(Arg.Any<Profile>());
        await _unitOfWorkMock.Received(1).CommitChangesAsync();
    }
}