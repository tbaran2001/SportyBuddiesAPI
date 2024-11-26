using AutoMapper;
using MediatR;
using SportyBuddies.Application.Common.DTOs.Conversation;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Domain.Buddies;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Common.Interfaces;
using SportyBuddies.Domain.Common.Interfaces.Services;
using SportyBuddies.Domain.Conversations;
using SportyBuddies.Domain.Services;

namespace SportyBuddies.Application.Features.Conversations.Commands.CreateConversation;

public class CreateConversationCommandHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IConversationService conversationService)
    : IRequestHandler<CreateConversationCommand, CreateConversationResponse>
{
    public async Task<CreateConversationResponse> Handle(CreateConversationCommand command,
        CancellationToken cancellationToken)
    {
        var conversation = await conversationService.CreateConversationAsync(command.CreatorId, command.ParticipantId);

        await unitOfWork.CommitChangesAsync();

        return mapper.Map<CreateConversationResponse>(conversation);
    }
}