using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Serilog;
using SportyBuddies.Api.Extensions;
using SportyBuddies.Api.Middlewares;
using SportyBuddies.Application;
using SportyBuddies.Application.Hubs;
using SportyBuddies.Identity;
using SportyBuddies.Identity.Models;
using SportyBuddies.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfig) =>
    loggerConfig.ReadFrom.Configuration(context.Configuration));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddIdentity(builder.Configuration);

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


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
    //app.ApplyMigrations();
    //app.ApplyIdentityMigrations();
}

app.UseHttpsRedirection();
app.UseMiddleware<RequestContextLoggingMiddleware>();
app.UseSerilogRequestLogging();
app.UseMiddleware<ExceptionHandlingMiddleware>();
//app.AddInfrastructureMiddleware();

app.MapGroup("/api").MapIdentityApi<ApplicationUser>();
app.MapPost("/api/logout", async (ClaimsPrincipal user, SignInManager<ApplicationUser> signInManager) =>
{
    await signInManager.SignOutAsync();
    return TypedResults.Ok();
});

app.UseCors("React");
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHub<ChatHub>("chatHub");

app.Run();