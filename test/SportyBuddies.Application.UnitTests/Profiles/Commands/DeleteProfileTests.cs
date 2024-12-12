using NSubstitute;
using SportyBuddies.Application.Authentication;
using SportyBuddies.Application.Features.Profiles.Commands.DeleteProfile;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using SportyBuddies.Domain.Profiles;

namespace SportyBuddies.Application.UnitTests.Profiles.Commands;

public class DeleteProfileTests
{
    private readonly DeleteProfileCommand _command = new();
    private readonly DeleteProfileCommandHandler _handler;
    private readonly IProfilesRepository _profilesRepositoryMock;
    private readonly IUnitOfWork _unitOfWorkMock;
    private readonly IUserContext _userContextMock;
    
    public DeleteProfileTests()
    {
        _profilesRepositoryMock = Substitute.For<IProfilesRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();
        _userContextMock = Substitute.For<IUserContext>();

        _handler = new DeleteProfileCommandHandler(_profilesRepositoryMock, _unitOfWorkMock, _userContextMock);
    }
    
    [Fact]
    public async Task Handle_ShouldDeleteUser_WhenValidRequest()
    {
        // Arrange
        var user = Profile.Create(Guid.NewGuid());

        var currentUser = new CurrentUser(user.Id, "", []);
        _userContextMock.GetCurrentUser().Returns(currentUser);

        _profilesRepositoryMock.GetProfileByIdAsync(currentUser.Id).Returns(user);
        
        // Act
        await _handler.Handle(_command, default);
        
        // Assert
        _profilesRepositoryMock.Received(1).RemoveProfile(user);
        await _unitOfWorkMock.Received(1).CommitChangesAsync();
    }
}