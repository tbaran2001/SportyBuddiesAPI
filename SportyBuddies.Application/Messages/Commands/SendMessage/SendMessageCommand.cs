using MediatR;

namespace SportyBuddies.Application.Messages.Commands.SendMessage;

public record SendMessageCommand(Guid SenderId, Guid RecipientId, string Content) : IRequest;