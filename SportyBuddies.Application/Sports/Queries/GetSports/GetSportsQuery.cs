using MediatR;
using SportyBuddies.Application.Common.DTOs.Sport;
using SportyBuddies.Application.Common.Interfaces;

namespace SportyBuddies.Application.Sports.Queries.GetSports;

public record GetSportsQuery : ICachedQuery<List<SportResponse>>
{
    public string CacheKey => "GetSportsQuery";
    public TimeSpan? Expiration => null;
}