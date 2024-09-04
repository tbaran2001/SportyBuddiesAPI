using MediatR;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Application.Exceptions;

namespace SportyBuddies.Application.Users.Commands.DeleteUser;

public class DeleteUserCommandHandler: IRequestHandler<DeleteUserCommand>
{
    private readonly IUsersRepository _sportsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserCommandHandler(IUsersRepository sportsRepository, IUnitOfWork unitOfWork)
    {
        _sportsRepository = sportsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        var user = await _sportsRepository.GetByIdAsync(command.UserId);

        if (user == null) throw new NotFoundException(nameof(user), command.UserId.ToString());

        _sportsRepository.Remove(user);
        await _unitOfWork.CommitChangesAsync();
    }
}