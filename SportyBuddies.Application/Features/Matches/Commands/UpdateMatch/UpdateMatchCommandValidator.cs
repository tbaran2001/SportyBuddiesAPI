using FluentValidation;

namespace SportyBuddies.Application.Features.Matches.Commands.UpdateMatch;

public class UpdateMatchCommandValidator: AbstractValidator<UpdateMatchCommand>
{
    public UpdateMatchCommandValidator()
    {
        RuleFor(x => x.MatchId).NotEmpty();
        RuleFor(x => x.Swipe).IsInEnum();
    }
}