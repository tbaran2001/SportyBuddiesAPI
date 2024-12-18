﻿using AutoMapper;
using FluentAssertions;
using NSubstitute;
using SportyBuddies.Application.Authentication;
using SportyBuddies.Application.Common.DTOs.Profile;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Application.Features.Profiles.Commands.UpdateProfile;
using SportyBuddies.Application.Mappings;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using SportyBuddies.Domain.Profiles;
using Profile = SportyBuddies.Domain.Profiles.Profile;

namespace SportyBuddies.Application.UnitTests.Profiles.Commands;

public class UpdateProfileTests
{
    private readonly UpdateProfileCommand _command = new("username", "description", Gender.Male,
        new DateOnly(2000, 1, 1));

    private readonly UpdateProfileCommandHandler _handler;
    private readonly IProfilesRepository _profilesRepositoryMock;
    private readonly IUnitOfWork _unitOfWorkMock;
    private readonly IUserContext _userContextMock;

    public UpdateProfileTests()
    {
        var configurationProvider = new MapperConfiguration(cfg => { cfg.AddProfile<ProfileMappingProfile>(); });
        var mapper = configurationProvider.CreateMapper();

        _profilesRepositoryMock = Substitute.For<IProfilesRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();
        _userContextMock = Substitute.For<IUserContext>();

        _handler = new UpdateProfileCommandHandler(_profilesRepositoryMock, mapper, _unitOfWorkMock, _userContextMock);
    }

    [Fact]
    public async Task Handle_ShouldUpdateUser_WhenValidRequest()
    {
        // Arrange
        var user = Profile.Create(Guid.NewGuid());

        var currentUser = new CurrentUser(user.Id, "", []);
        _userContextMock.GetCurrentUser().Returns(currentUser);

        _profilesRepositoryMock.GetProfileByIdAsync(currentUser.Id).Returns(user);
        // Act
        var result = await _handler.Handle(_command, default);

        // Assert
        user.Name.Should().Be(_command.Name);
        user.Description.Should().Be(_command.Description);
        user.Gender.Should().Be(_command.Gender);
        user.DateOfBirth.Should().Be(_command.DateOfBirth);

        await _unitOfWorkMock.Received(1).CommitChangesAsync();

        result.Should().BeOfType<ProfileResponse>();
    }

    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenUserDoesNotExist()
    {
        // Arrange
        var currentUser = new CurrentUser(Guid.NewGuid(), "", []);
        _userContextMock.GetCurrentUser().Returns(currentUser);

        _profilesRepositoryMock.GetProfileByIdAsync(currentUser.Id).Returns((Profile?)null);

        // Act
        var act = async () => await _handler.Handle(_command, default);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}