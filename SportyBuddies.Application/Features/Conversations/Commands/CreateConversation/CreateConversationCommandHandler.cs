using AutoMapper;
using MediatR;
using SportyBuddies.Application.Common.DTOs.Conversation;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Domain.Buddies;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Conversations;

namespace SportyBuddies.Application.Features.Conversations.Commands.CreateConversation;

public class CreateConversationCommandHandler(
    IConversationsRepository conversationsRepository,
    IBuddiesRepository buddiesRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<CreateConversationCommand, CreateConversationResponse>
{
    public async Task<CreateConversationResponse> Handle(CreateConversationCommand command, CancellationToken cancellationToken)
    {
        if(!await conversationsRepository.AreParticipantsBuddiesAsync(command.ParticipantIds))
            throw new BadRequestException("Participants are not buddies");

        if(await conversationsRepository.UsersHaveConversation(command.ParticipantIds))
            throw new BadRequestException("Conversation already exists");

        var conversation = Conversation.Create(command.CreatorId, command.ParticipantIds);

        var buddies=await buddiesRepository.GetRelatedBuddies(command.ParticipantIds.First(),command.ParticipantIds.Last());
        foreach (var buddy in buddies)
        {
            buddy.SetConversation(conversation);
        }

        await conversationsRepository.AddAsync(conversation);
        await unitOfWork.CommitChangesAsync();

        return mapper.Map<CreateConversationResponse>(conversation);
    }
}