using FluentValidation;

namespace SportyBuddies.Application.Sports.Commands.CreateSport;

public class CreateSportCommandValidator : AbstractValidator<CreateSportCommand>
{
    public CreateSportCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(1).MaximumLength(10);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(25);
    }
}