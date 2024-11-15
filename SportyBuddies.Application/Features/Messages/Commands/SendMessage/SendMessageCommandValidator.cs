using FluentValidation;
using SportyBuddies.Domain.Buddies;

namespace SportyBuddies.Application.Features.Messages.Commands.SendMessage;

public class SendMessageCommandValidator: AbstractValidator<SendMessageCommand>
{
    public SendMessageCommandValidator(IBuddiesRepository buddiesRepository)
    {
        RuleFor(x => x.RecipientId).NotEmpty();
        RuleFor(x => x.SenderId).NotEmpty();
        RuleFor(x => x.Content).NotEmpty()
            .MaximumLength(500);
        
        RuleFor(x=> x)
            .MustAsync(async (command, _) => await buddiesRepository.AreUsersBuddiesAsync(command.SenderId, command.RecipientId))
            .WithMessage("You can only send messages to your buddies.");
    }
}