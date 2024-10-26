using AutoMapper;
using SportyBuddies.Application.Common.DTOs.Message;
using SportyBuddies.Domain.Messages;

namespace SportyBuddies.Application.Mappings;

public class MessageMappingProfile : Profile
{
    public MessageMappingProfile()
    {
        CreateMap<Message, MessageResponse>();
    }
}