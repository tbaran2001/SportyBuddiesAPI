using AutoMapper;
using SportyBuddies.Application.Common.DTOs.Conversation;
using SportyBuddies.Application.Common.DTOs.Message;
using SportyBuddies.Domain.Conversations;

namespace SportyBuddies.Application.Mappings;

public class ConversationMappingProfile : Profile
{
    public ConversationMappingProfile()
    {
        CreateMap<Conversation, ConversationResponse>();
        CreateMap<Message, MessageResponse>();
    }
}