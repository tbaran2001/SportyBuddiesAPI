using AutoMapper;
using FluentAssertions;
using NSubstitute;
using SportyBuddies.Application.Common.DTOs;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Application.Mappings;
using SportyBuddies.Application.Sports.Commands.CreateSport;
using SportyBuddies.Domain.Sports;

namespace SportyBuddies.Application.UnitTests.Sports.Commands;

public class CreateSportCommandHandlerTests
{
    private readonly CreateSportCommand _createSportCommand;
    private readonly IMapper _mapper;
    private readonly ISportsRepository _sportsRepository = Substitute.For<ISportsRepository>();
    private readonly CreateSportCommandHandler _sut;
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly CreateSportCommandValidator _validator;

    public CreateSportCommandHandlerTests()
    {
        var configurationProvider = new MapperConfiguration(cfg => { cfg.AddProfile<SportMappingProfile>(); });
        _mapper = configurationProvider.CreateMapper();
        _sut = new CreateSportCommandHandler(_sportsRepository, _unitOfWork, _mapper);
        _createSportCommand = new CreateSportCommand("dawd", "Football description");
        _validator = new CreateSportCommandValidator();
    }

    [Fact]
    public async Task Handle_ShouldAddSportAndCommit_WhenRequestIsValid()
    {
        var validationResult = await _validator.ValidateAsync(_createSportCommand);
        validationResult.IsValid.Should().BeTrue();

        var result = await _sut.Handle(_createSportCommand, CancellationToken.None);

        _sportsRepository.GetAllAsync().Returns(new List<Sport>
        {
            _mapper.Map<Sport>(result.Value)
        });

        var sports = await _sportsRepository.GetAllAsync();

        sports.Should().HaveCount(1);

        await _sportsRepository.Received(1).AddAsync(Arg.Is<Sport>(x =>
            x.Name == _createSportCommand.Name && x.Description == _createSportCommand.Description));
        await _unitOfWork.Received(1).CommitChangesAsync();

        result.IsError.Should().BeFalse();
        result.Value.Should().BeOfType<SportDto>();
    }
}