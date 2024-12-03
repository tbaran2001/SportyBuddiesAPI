using AutoMapper;
using MediatR;
using SportyBuddies.Application.Authentication;
using SportyBuddies.Application.Common.DTOs.Conversation;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Common.Interfaces.Services;

namespace SportyBuddies.Application.Features.Conversations.Commands.CreateConversation;

public class CreateConversationCommandHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IConversationService conversationService,
    IUserContext userContext)
    : IRequestHandler<CreateConversationCommand, CreateConversationResponse>
{
    public async Task<CreateConversationResponse> Handle(CreateConversationCommand command,
        CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();
        var conversation = await conversationService.CreateConversationAsync(currentUser!.Id, command.ParticipantId);

        await unitOfWork.CommitChangesAsync();

        return mapper.Map<CreateConversationResponse>(conversation);
    }
}