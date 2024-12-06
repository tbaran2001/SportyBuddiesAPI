using AutoMapper;
using FluentAssertions;
using NSubstitute;
using SportyBuddies.Application.Common.DTOs.Sport;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Application.Features.Sports.Commands.CreateSport;
using SportyBuddies.Application.Mappings;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Common.Interfaces;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using SportyBuddies.Domain.Sports;

namespace SportyBuddies.Application.UnitTests.Sports.Commands;

public class CreateSportTests
{
    private readonly CreateSportCommand _command = new("Football", "Football description");
    private readonly CreateSportCommandHandler _handler;
    private readonly ISportsRepository _sportsRepositoryMock;
    private readonly IUnitOfWork _unitOfWorkMock;

    public CreateSportTests()
    {
        var configurationProvider = new MapperConfiguration(cfg => { cfg.AddProfile<SportMappingProfile>(); });
        var mapper = configurationProvider.CreateMapper();

        _sportsRepositoryMock = Substitute.For<ISportsRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();
        _handler = new CreateSportCommandHandler(_sportsRepositoryMock, _unitOfWorkMock, mapper);
    }

    [Fact]
    public async Task Handle_ShouldReturnSport_WhenSportIsCreated()
    {
        // Act
        var result = await _handler.Handle(_command, default);

        // Assert
        result.Should().BeOfType<SportResponse>();
        result.Name.Should().Be(_command.Name);
        result.Description.Should().Be(_command.Description);
        
        await _sportsRepositoryMock.Received(1).AddSportAsync(Arg.Any<Sport>());
        await _unitOfWorkMock.Received(1).CommitChangesAsync();
    }
    
    [Fact]
    public async Task Handle_ShouldThrowException_WhenSportAlreadyExists()
    {
        // Arrange
        _sportsRepositoryMock.SportNameExistsAsync(_command.Name).Returns(true);

        // Act
        Func<Task> act = async () => await _handler.Handle(_command, default);

        // Assert
        await act.Should().ThrowAsync<ConflictException>();
    }
}