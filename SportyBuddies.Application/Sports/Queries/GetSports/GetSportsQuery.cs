using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.DTOs.Sport;

namespace SportyBuddies.Application.Sports.Queries.GetSports;

public record GetSportsQuery : IRequest<ErrorOr<List<SportResponse>>>;