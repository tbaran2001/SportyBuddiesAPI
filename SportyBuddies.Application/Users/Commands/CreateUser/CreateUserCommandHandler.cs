using AutoMapper;
using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.DTOs;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler(IUsersRepository usersRepository, IMapper mapper, IUnitOfWork unitOfWork)
    : IRequestHandler<CreateUserCommand, ErrorOr<UserWithSportsResponse>>
{
    public async Task<ErrorOr<UserWithSportsResponse>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var user = mapper.Map<User>(command);

        await usersRepository.AddUserAsync(user);
        await unitOfWork.CommitChangesAsync();

        return mapper.Map<UserWithSportsResponse>(user);
    }
}