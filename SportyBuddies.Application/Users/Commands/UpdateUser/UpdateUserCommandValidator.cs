using FluentValidation;

namespace SportyBuddies.Application.Users.Commands.UpdateUser;

public class UpdateUserCommandValidator: AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(4);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(6);
    }
}