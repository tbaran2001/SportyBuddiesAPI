using AutoMapper;
using MediatR;
using SportyBuddies.Application.Common.DTOs.User;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler(IUsersRepository usersRepository, IMapper mapper, IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateUserCommand, UserResponse>
{
    public async Task<UserResponse> Handle(UpdateUserCommand command,
        CancellationToken cancellationToken)
    {
        var user = await usersRepository.GetUserByIdAsync(command.UserId);

        if (user == null)
            throw new NotFoundException(nameof(user), command.UserId.ToString());
        
        user.Update(command.Name, command.Description, command.DateOfBirth,command.Gender);
        await unitOfWork.CommitChangesAsync();

        return mapper.Map<UserResponse>(user);
    }
}