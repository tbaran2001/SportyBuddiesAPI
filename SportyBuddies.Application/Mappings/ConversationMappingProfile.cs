using AutoMapper;
using SportyBuddies.Application.Common.DTOs.Conversation;
using SportyBuddies.Domain.Conversations;

namespace SportyBuddies.Application.Mappings;

public class ConversationMappingProfile : Profile
{
    public ConversationMappingProfile()
    {
        CreateMap<Conversation, CreateConversationResponse>();
        CreateMap<Conversation, ConversationResponse>()
            .ForMember(dest => dest.Participants, opt => opt.MapFrom(src => src.Participants));
        CreateMap<Message, MessageResponse>();
        CreateMap<Participant, ParticipantResponse>()
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));
    }
}