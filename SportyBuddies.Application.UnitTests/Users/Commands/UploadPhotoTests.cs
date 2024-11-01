﻿using FluentAssertions;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using SportyBuddies.Application.Common.Services;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Application.Users.Commands.UploadPhoto;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.UnitTests.Users.Commands;

public class UploadPhotoTests
{
    private readonly UploadPhotoCommand _command = new(Guid.NewGuid(), false, null!);
    private readonly UploadPhotoCommandHandler _handler;
    private readonly IFileStorageService _fileStorageServiceMock;
    private readonly IUsersRepository _usersRepositoryMock;
    private readonly IUnitOfWork _unitOfWorkMock;
    private readonly IFormFile _fileMock;
    
    public UploadPhotoTests()
    {
        _fileStorageServiceMock = Substitute.For<IFileStorageService>();
        _usersRepositoryMock = Substitute.For<IUsersRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();
        _handler = new UploadPhotoCommandHandler(_fileStorageServiceMock, _usersRepositoryMock, _unitOfWorkMock);
        
        _fileMock = Substitute.For<IFormFile>();
    }
    
    [Fact]
    public async Task Handle_ShouldReturnUrl_WhenValidRequest()
    {
        // Arrange
        var user = User.Create(_command.UserId);
        _usersRepositoryMock.GetUserByIdWithPhotosAsync(_command.UserId).Returns(user);
        _fileStorageServiceMock.SaveFileAsync(user.Id, _fileMock, Arg.Any<Guid>()).Returns("url");
        
        // Act
        var result = await _handler.Handle(_command, default);
        
        // Assert
        user.Photos.Should().HaveCount(1);
        
        await _unitOfWorkMock.Received(1).CommitChangesAsync();
    }
    
    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenUserDoesNotExist()
    {
        // Arrange
        _usersRepositoryMock.GetUserByIdWithPhotosAsync(_command.UserId).Returns((User?)null);
        
        // Act
        var act = async () => await _handler.Handle(_command, default);
        
        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}