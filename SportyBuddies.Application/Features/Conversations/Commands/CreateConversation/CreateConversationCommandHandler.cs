using AutoMapper;
using MediatR;
using SportyBuddies.Application.Common.DTOs.Conversation;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Conversations;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.Features.Conversations.Commands.CreateConversation;

public class CreateConversationCommandHandler(
    IConversationsRepository conversationsRepository,
    IUsersRepository usersRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<CreateConversationCommand, ConversationResponse>
{
    public async Task<ConversationResponse> Handle(CreateConversationCommand command, CancellationToken cancellationToken)
    {
        var conversation = Conversation.Create(command.CreatorId, command.ParticipantIds);

        await conversationsRepository.AddAsync(conversation);
        await unitOfWork.CommitChangesAsync();

        return mapper.Map<ConversationResponse>(conversation);
    }
}