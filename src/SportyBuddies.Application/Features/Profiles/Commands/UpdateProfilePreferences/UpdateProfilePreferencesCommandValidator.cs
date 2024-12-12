using FluentValidation;

namespace SportyBuddies.Application.Features.Profiles.Commands.UpdateProfilePreferences;

public class UpdateProfilePreferencesCommandValidator: AbstractValidator<UpdateProfilePreferencesCommand>
{
    public UpdateProfilePreferencesCommandValidator()
    {
        RuleFor(x => x.MinAge)
            .NotEmpty()
            .GreaterThanOrEqualTo(18)
            .LessThanOrEqualTo(120);
        RuleFor(x => x.MaxAge)
            .GreaterThanOrEqualTo(18)
            .LessThanOrEqualTo(120);
        RuleFor(x => x.MinAge)
            .LessThanOrEqualTo(x => x.MaxAge);
        RuleFor(x => x.Gender)
            .IsInEnum();
        RuleFor(x => x.MaxDistance)
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(100);
    }
}