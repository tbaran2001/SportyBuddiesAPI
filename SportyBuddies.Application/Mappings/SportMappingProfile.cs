﻿using AutoMapper;
using SportyBuddies.Application.Sports.Commands.CreateSport;
using SportyBuddies.Domain.Sports;

namespace SportyBuddies.Application.Mappings;

public class SportMappingProfile : Profile
{
    public SportMappingProfile()
    {
        CreateMap<CreateSportCommand, Sport>();
    }
}