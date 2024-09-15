using AutoMapper;
using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.DTOs;
using SportyBuddies.Application.Common.Interfaces;

namespace SportyBuddies.Application.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ErrorOr<UserDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUsersRepository _usersRepository;

    public UpdateUserCommandHandler(IUsersRepository usersRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _usersRepository = usersRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<UserDto>> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetByIdAsync(command.UserId);

        if (user == null) return Error.NotFound();

        _mapper.Map(command, user);
        await _unitOfWork.CommitChangesAsync();

        return _mapper.Map<UserDto>(user);
    }
}