using MediatR;
using SportyBuddies.Application.Common.DTOs.Message;

namespace SportyBuddies.Application.Features.Messages.Queries.GetLastUserMessages;

public record GetLastUserMessagesQuery(Guid UserId) : IRequest<List<MessageResponse>>;