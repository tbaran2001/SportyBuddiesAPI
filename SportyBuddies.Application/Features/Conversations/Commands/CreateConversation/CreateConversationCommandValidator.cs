using FluentValidation;
using SportyBuddies.Domain.Buddies;

namespace SportyBuddies.Application.Features.Conversations.Commands.CreateConversation;

public class CreateConversationCommandValidator : AbstractValidator<CreateConversationCommand>
{
    public CreateConversationCommandValidator()
    {
        RuleFor(x => x.CreatorId).NotEmpty();
        RuleFor(x => x.ParticipantId).NotEmpty();
    }
}