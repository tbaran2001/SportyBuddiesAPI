using AutoMapper;
using MediatR;
using SportyBuddies.Application.Common.DTOs;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Application.Exceptions;

namespace SportyBuddies.Application.Users.Queries.GetUser;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto>
{
    private readonly IMapper _mapper;
    private readonly IUsersRepository _usersRepository;

    public GetUserQueryHandler(IUsersRepository usersRepository, IMapper mapper)
    {
        _usersRepository = usersRepository;
        _mapper = mapper;
    }


    public async Task<UserDto> Handle(GetUserQuery query, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetByIdAsync(query.UserId);

        if (user == null) throw new NotFoundException(nameof(user), query.UserId.ToString());

        return _mapper.Map<UserDto>(user);
    }
}