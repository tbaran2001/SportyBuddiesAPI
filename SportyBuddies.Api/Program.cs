using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using SportyBuddies.Application;
using SportyBuddies.Application.Hubs;
using SportyBuddies.Identity;
using SportyBuddies.Identity.Models;
using SportyBuddies.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddApplication()
    .AddInfrastructure()
    .AddIdentity();
builder.Services.AddProblemDetails();
builder.Services.AddHttpContextAccessor();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddCors(options =>
{
    options.AddPolicy("React",
        corsPolicyBuilder => corsPolicyBuilder.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
});

builder.Services.AddSignalR();

var app = builder.Build();

app.UseExceptionHandler();
//app.AddInfrastructureMiddleware();

app.MapGroup("/api").MapIdentityApi<ApplicationUser>();
app.MapPost("/api/logout", async (ClaimsPrincipal user, SignInManager<ApplicationUser> signInManager) =>
{
    await signInManager.SignOutAsync();
    return TypedResults.Ok();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseExceptionHandler("/errors");
app.UseHttpsRedirection();

app.UseCors("React");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapPost("broadcast", async (HubMessage message, IHubContext<ChatHub, IChatClient> context) =>
{
    await context.Clients.All.ReceiveMessage(message);
    return Results.NoContent();
});

//signalR
app.MapHub<ChatHub>("chatHub");

app.Run();