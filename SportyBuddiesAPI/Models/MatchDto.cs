﻿namespace SportyBuddiesAPI.Models;

public class MatchDto
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public string MatchedUserId { get; set; }
    public UserDto User { get; set; } = new UserDto();
    public UserDto MatchedUser { get; set; } = new UserDto();
    public DateTime MatchDateTime { get; set; }
    public Swipe? Swipe { get; set; }
    public DateTime? SwipeDateTime { get; set; }
}