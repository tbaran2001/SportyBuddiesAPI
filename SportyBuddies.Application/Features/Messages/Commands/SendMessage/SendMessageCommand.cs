using MediatR;

namespace SportyBuddies.Application.Features.Messages.Commands.SendMessage;

public record SendMessageCommand(Guid SenderId, Guid RecipientId, string Content) : IRequest;