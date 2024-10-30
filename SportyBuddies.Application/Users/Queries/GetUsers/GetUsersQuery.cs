using MediatR;
using SportyBuddies.Application.Common.Interfaces;

namespace SportyBuddies.Application.Users.Queries.GetUsers;

public record GetUsersQuery(bool IncludeSports) : ICachedQuery<object>
{
    public string CacheKey => "GetUsersQuery";
    public TimeSpan? Expiration => null;
}