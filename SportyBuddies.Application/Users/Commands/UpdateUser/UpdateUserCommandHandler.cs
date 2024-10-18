using AutoMapper;
using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.DTOs;
using SportyBuddies.Application.Common.Interfaces;

namespace SportyBuddies.Application.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler(IUsersRepository usersRepository, IMapper mapper, IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateUserCommand, ErrorOr<UserWithSportsResponse>>
{
    public async Task<ErrorOr<UserWithSportsResponse>> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        var user = await usersRepository.GetUserByIdAsync(command.UserId);

        if (user == null) 
            return Error.NotFound();

        mapper.Map(command, user);
        await unitOfWork.CommitChangesAsync();

        return mapper.Map<UserWithSportsResponse>(user);
    }
}