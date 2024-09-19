using ErrorOr;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using NSubstitute;
using SportyBuddies.Application.Common.Behaviors;
using SportyBuddies.Application.Common.DTOs;
using SportyBuddies.Application.Sports.Commands.CreateSport;
using TestCommon.Sports;

namespace SportyBuddies.Application.UnitTests.Common.Behaviors;

public class ValidationBehaviorTests
{
    private readonly RequestHandlerDelegate<ErrorOr<SportDto>> _mockNextBehavior;
    private readonly IValidator<CreateSportCommand> _mockValidator;
    private readonly ValidationBehavior<CreateSportCommand, ErrorOr<SportDto>> _validationBehavior;

    public ValidationBehaviorTests()
    {
        // Create a next behavior (mock)
        _mockNextBehavior = Substitute.For<RequestHandlerDelegate<ErrorOr<SportDto>>>();

        // Create validator (mock)
        _mockValidator = Substitute.For<IValidator<CreateSportCommand>>();

        // Create validation behavior (SUT)
        _validationBehavior = new ValidationBehavior<CreateSportCommand, ErrorOr<SportDto>>(_mockValidator);
    }

    [Fact]
    public async Task InvokeBehavior_WhenValidatorResultIsValid_ShouldInvokeNextBehavior()
    {
        // Arrange
        var createSportRequest = SportCommandFactory.CreateCreateSportCommand();
        var sport = SportFactory.CreateSport();

        _mockValidator
            .ValidateAsync(createSportRequest, Arg.Any<CancellationToken>())
            .Returns(new ValidationResult());

        _mockNextBehavior.Invoke().Returns(sport);

        // Act
        var result = await _validationBehavior.Handle(createSportRequest, _mockNextBehavior, CancellationToken.None);

        // Assert
        result.IsError.Should().BeFalse();
        result.Value.Should().Be(sport);
    }

    [Fact]
    public async Task InvokeBehavior_WhenValidatorResultIsNotValid_ShouldReturnListOfErrors()
    {
        // Arrange
        var createSportRequest = SportCommandFactory.CreateCreateSportCommand();
        var validationFailures = new List<ValidationFailure> { new("foo", "bad foo") };

        _mockValidator
            .ValidateAsync(createSportRequest, Arg.Any<CancellationToken>())
            .Returns(new ValidationResult(validationFailures));

        // Act
        var result = await _validationBehavior.Handle(createSportRequest, _mockNextBehavior, CancellationToken.None);

        // Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Code.Should().Be("foo");
        result.FirstError.Description.Should().Be("bad foo");
    }
}