namespace SportyBuddies.Application.Common.DTOs;

public record UserDto(Guid Id, string Name, string Description, DateTime LastActive, ICollection<SportDto> Sports);