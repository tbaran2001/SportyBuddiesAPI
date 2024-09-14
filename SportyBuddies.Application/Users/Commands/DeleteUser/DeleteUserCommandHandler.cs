using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.Interfaces;

namespace SportyBuddies.Application.Users.Commands.DeleteUser;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ErrorOr<Deleted>>
{
    private readonly IUsersRepository _sportsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserCommandHandler(IUsersRepository sportsRepository, IUnitOfWork unitOfWork)
    {
        _sportsRepository = sportsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Deleted>> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        var user = await _sportsRepository.GetByIdAsync(command.UserId);

        if (user == null) Error.NotFound();

        _sportsRepository.Remove(user);
        await _unitOfWork.CommitChangesAsync();

        return Result.Deleted;
    }
}