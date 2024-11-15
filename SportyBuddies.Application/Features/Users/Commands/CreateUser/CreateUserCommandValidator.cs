using FluentValidation;

namespace SportyBuddies.Application.Features.Users.Commands.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(1).MaximumLength(4);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(6);
    }
}