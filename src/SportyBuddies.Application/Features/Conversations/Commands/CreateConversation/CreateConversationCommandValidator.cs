using FluentValidation;

namespace SportyBuddies.Application.Features.Conversations.Commands.CreateConversation;

public class CreateConversationCommandValidator : AbstractValidator<CreateConversationCommand>
{
    public CreateConversationCommandValidator()
    {
        RuleFor(x => x.ParticipantId).NotEmpty();
    }
}