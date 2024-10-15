using AutoMapper;
using SportyBuddies.Application.Common.DTOs;
using SportyBuddies.Domain.Messages;

namespace SportyBuddies.Application.Mappings;

public class MessageMappingProfile : Profile
{
    public MessageMappingProfile()
    {
        CreateMap<Message, MessageResponse>()
            .ForMember(dest => dest.SenderId, opt => opt.MapFrom(src => src.Sender.Id))
            .ForMember(dest => dest.RecipientId, opt => opt.MapFrom(src => src.Recipient.Id));
    }
}