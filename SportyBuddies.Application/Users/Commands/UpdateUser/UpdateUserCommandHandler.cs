using AutoMapper;
using MediatR;
using SportyBuddies.Application.Common.DTOs.User;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Application.Exceptions;

namespace SportyBuddies.Application.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler(IUsersRepository usersRepository, IMapper mapper, IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateUserCommand, UserWithSportsResponse>
{
    public async Task<UserWithSportsResponse> Handle(UpdateUserCommand command,
        CancellationToken cancellationToken)
    {
        var user = await usersRepository.GetUserByIdAsync(command.UserId);

        if (user == null)
            throw new NotFoundException(nameof(user), command.UserId.ToString());

        mapper.Map(command, user);
        await unitOfWork.CommitChangesAsync();

        return mapper.Map<UserWithSportsResponse>(user);
    }
}