using FluentAssertions;
using NSubstitute;
using SportyBuddies.Application.Authentication;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Application.Features.ProfileSports.Commands.AddProfileSport;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using SportyBuddies.Domain.Profiles;
using SportyBuddies.Domain.Sports;

namespace SportyBuddies.Application.UnitTests.ProfileSports.Commands;

public class AddProfileSportTests
{
    private readonly AddProfileSportCommand _command= new(Guid.NewGuid());
    private readonly AddProfileSportCommandHandler _handler;
    private readonly IProfilesRepository _profilesRepositoryMock;
    private readonly ISportsRepository _sportsRepositoryMock;
    private readonly IUnitOfWork _unitOfWorkMock;
    private readonly IUserContext _userContextMock;

    public AddProfileSportTests()
    {
        _profilesRepositoryMock = Substitute.For<IProfilesRepository>();
        _sportsRepositoryMock = Substitute.For<ISportsRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();
        _userContextMock = Substitute.For<IUserContext>();

        _handler = new AddProfileSportCommandHandler(_profilesRepositoryMock, _sportsRepositoryMock, _unitOfWorkMock,
            _userContextMock);
    }
    
    [Fact]
    public async Task Handle_Should_AddSportToUser()
    {
        // Arrange
        var user = Profile.Create(Guid.NewGuid());

        var currentUser = new CurrentUser(user.Id, "", []);
        _userContextMock.GetCurrentUser().Returns(currentUser);

        var sport = Sport.Create("Football", "Description");
        _profilesRepositoryMock.GetProfileByIdWithSportsAsync(currentUser.Id).Returns(user);
        _sportsRepositoryMock.GetSportByIdAsync(_command.SportId).Returns(sport);
        
        // Act
        await _handler.Handle(_command, default);
        
        // Assert
        user.Sports.Should().Contain(sport);
        
        await _unitOfWorkMock.Received(1).CommitChangesAsync();
    }
    
    [Fact]
    public async Task Handle_Should_ThrowNotFoundException_WhenUserNotFound()
    {
        // Arrange
        var currentUser = new CurrentUser(Guid.NewGuid(),  "", []);
        _userContextMock.GetCurrentUser().Returns(currentUser);

        _profilesRepositoryMock.GetProfileByIdWithSportsAsync(currentUser.Id).Returns((Profile?)null);
        
        // Act
        var act = async () => await _handler.Handle(_command, default);
        
        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
    
    [Fact]
    public async Task Handle_Should_ThrowNotFoundException_WhenSportNotFound()
    {
        // Arrange
        var currentUser = new CurrentUser(Guid.NewGuid(),  "", []);
        _userContextMock.GetCurrentUser().Returns(currentUser);

        _profilesRepositoryMock.GetProfileByIdWithSportsAsync(currentUser.Id).Returns(Profile.Create(Guid.NewGuid()));
        _sportsRepositoryMock.GetSportByIdAsync(_command.SportId).Returns((Sport?)null);
        
        // Act
        var act = async () => await _handler.Handle(_command, default);
        
        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}