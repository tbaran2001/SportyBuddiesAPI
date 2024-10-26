using AutoMapper;
using MediatR;
using SportyBuddies.Application.Common.DTOs.User;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler(IUsersRepository usersRepository, IMapper mapper, IUnitOfWork unitOfWork)
    : IRequestHandler<CreateUserCommand, UserWithSportsResponse>
{
    public async Task<UserWithSportsResponse> Handle(CreateUserCommand command,
        CancellationToken cancellationToken)
    {
        var user = mapper.Map<User>(command);

        await usersRepository.AddUserAsync(user);
        await unitOfWork.CommitChangesAsync();

        return mapper.Map<UserWithSportsResponse>(user);
    }
}