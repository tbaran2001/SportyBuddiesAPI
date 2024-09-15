using AutoMapper;
using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.DTOs;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ErrorOr<UserDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUsersRepository _usersRepository;

    public CreateUserCommandHandler(IUsersRepository usersRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _usersRepository = usersRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<UserDto>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(command);

        await _usersRepository.AddAsync(user);
        await _unitOfWork.CommitChangesAsync();

        return _mapper.Map<UserDto>(user);
    }
}