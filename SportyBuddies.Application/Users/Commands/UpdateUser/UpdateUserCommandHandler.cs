using AutoMapper;
using MediatR;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler: IRequestHandler<UpdateUserCommand, User>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserCommandHandler(IUsersRepository usersRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _usersRepository = usersRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<User> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        var validator = new UpdateUserCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        
        if (validationResult.IsValid == false) throw new ValidationException(validationResult.ToDictionary());
        
        var user = await _usersRepository.GetByIdAsync(command.UserId);
        
        if (user == null) throw new NotFoundException(nameof(User), command.UserId.ToString());
        
        _mapper.Map(command, user);
        
        await _unitOfWork.CommitChangesAsync();
        
        return user;
    }
}