using FluentValidation;

namespace SportyBuddies.Application.Users.Commands.CreateUser;

public class CreateUserCommandValidator: AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(4);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(6);
    }
}