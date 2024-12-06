using AutoMapper;
using MediatR;
using SportyBuddies.Application.Authentication;
using SportyBuddies.Application.Common.DTOs.User;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Common.Interfaces.Repositories;

namespace SportyBuddies.Application.Features.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler(
    IUsersRepository usersRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IUserContext userContext)
    : IRequestHandler<UpdateUserCommand, UserResponse>
{
    public async Task<UserResponse> Handle(UpdateUserCommand command,
        CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();

        var user = await usersRepository.GetUserByIdAsync(currentUser.Id);
        if (user == null)
            throw new NotFoundException(nameof(user), currentUser.Id.ToString());

        user.Update(command.Name, command.Description, command.DateOfBirth, command.Gender);
        await unitOfWork.CommitChangesAsync();

        return mapper.Map<UserResponse>(user);
    }
}