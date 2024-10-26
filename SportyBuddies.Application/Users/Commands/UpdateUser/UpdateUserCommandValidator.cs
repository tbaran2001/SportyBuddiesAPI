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
        RuleFor(x => x.DateOfBirth)
            .Must(dateOfBirth => DateTime.Now.Year - dateOfBirth.Year >= 18);
            
    }
}