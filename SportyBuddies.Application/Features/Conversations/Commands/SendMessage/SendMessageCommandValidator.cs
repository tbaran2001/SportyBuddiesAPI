using FluentValidation;

namespace SportyBuddies.Application.Features.Conversations.Commands.SendMessage;

public class SendMessageCommandValidator:AbstractValidator<SendMessageCommand>
{
    public SendMessageCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.ConversationId).NotEmpty();
        RuleFor(x => x.Content).NotEmpty()
            .MaximumLength(500);
    }
}