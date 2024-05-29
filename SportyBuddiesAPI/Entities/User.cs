﻿namespace SportyBuddiesAPI.Entities;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string? Description { get; set; }
}