﻿namespace Sport.API.Models;

public class Sport
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
}