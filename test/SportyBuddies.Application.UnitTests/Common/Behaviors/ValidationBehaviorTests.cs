﻿using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using NSubstitute;
using SportyBuddies.Application.Common.Behaviors;
using SportyBuddies.Application.Common.DTOs.Sport;
using SportyBuddies.Application.Features.Sports.Commands.CreateSport;
using TestCommon.Sports;
using ValidationException = SportyBuddies.Application.Exceptions.ValidationException;

namespace SportyBuddies.Application.UnitTests.Common.Behaviors;

public class ValidationBehaviorTests
{
    private readonly RequestHandlerDelegate<SportResponse> _mockNextBehavior;
    private readonly IValidator<CreateSportCommand> _mockValidator;
    private readonly ValidationBehavior<CreateSportCommand, SportResponse> _validationBehavior;

    public ValidationBehaviorTests()
    {
        // Create a next behavior (mock)
        _mockNextBehavior = Substitute.For<RequestHandlerDelegate<SportResponse>>();

        // Create validator (mock)
        _mockValidator = Substitute.For<IValidator<CreateSportCommand>>();

        // Create validation behavior (SUT)
        _validationBehavior = new ValidationBehavior<CreateSportCommand, SportResponse>(_mockValidator);
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
        result.Should().Be(sport);
    }

    [Fact]
    public async Task InvokeBehavior_WhenValidatorResultIsNotValid_ShouldThrowValidationException()
    {
        // Arrange
        var createSportRequest = SportCommandFactory.CreateCreateSportCommand();
        var validationFailures = new List<ValidationFailure> { new("foo", "bad foo") };

        _mockValidator
            .ValidateAsync(createSportRequest, Arg.Any<CancellationToken>())
            .Returns(new ValidationResult(validationFailures));

        // Act
        var act = new Func<Task>(() => _validationBehavior.Handle(createSportRequest, _mockNextBehavior, CancellationToken.None));
        
        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }
}