using AutoMapper;
using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.DTOs;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Domain.UserAggregate;

namespace SportyBuddies.Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler(IUsersRepository usersRepository, IMapper mapper, IUnitOfWork unitOfWork)
    : IRequestHandler<CreateUserCommand, ErrorOr<UserResponse>>
{
    public async Task<ErrorOr<UserResponse>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var user = User.Create(command.Name, command.Description, DateTime.UtcNow);

        await usersRepository.AddUserAsync(user);
        await unitOfWork.CommitChangesAsync();

        return mapper.Map<UserResponse>(user);
    }
}