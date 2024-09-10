using AutoMapper;
using MediatR;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler: IRequestHandler<CreateUserCommand, User>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandHandler(IUsersRepository usersRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _usersRepository = usersRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<User> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateUserCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        
        if (validationResult.IsValid == false) throw new ValidationException(validationResult.ToDictionary());
        
        var user = _mapper.Map<User>(command);
        
        await _usersRepository.AddAsync(user);
        await _unitOfWork.CommitChangesAsync();

        return user;
    }
}