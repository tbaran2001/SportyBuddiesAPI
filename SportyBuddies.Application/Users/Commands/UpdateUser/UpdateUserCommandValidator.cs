using FluentValidation;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.Users.Commands.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(10);
        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(60);
        RuleFor(x => x.Gender)
            .Must(gender => Enum.IsDefined(typeof(Gender), gender) && gender != Gender.Unknown);
        //make date of birth validation cant be less than 18 years old
        RuleFor(x => x.DateOfBirth)
            .NotEmpty()
            .LessThan(DateTime.Now.AddYears(-18))
            .GreaterThan(DateTime.Now.AddYears(-100));
            
    }
}