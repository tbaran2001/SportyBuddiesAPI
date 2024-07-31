using Microsoft.EntityFrameworkCore;
using SportyBuddiesAPI.DbContexts;
using SportyBuddiesAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddNewtonsoftJson();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        builder => builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SportyBuddiesContext>(dbContextOptions =>
{
    dbContextOptions.UseSqlite(
        builder.Configuration["ConnectionStrings:SportyBuddiesDBConnectionString"]);
});

builder.Services.AddScoped<ISportyBuddiesRepository, SportyBuddiesRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Configuration.GetValue<bool>("UseDeveloperExceptionPage"))
    app.UseDeveloperExceptionPage();
else
    app.UseExceptionHandler("/error");


app.UseHttpsRedirection();

app.UseCors("AllowReactApp");

app.UseAuthorization();

app.MapGet("/error", () => Results.Problem());
app.MapGet("/error/test", () =>
{
    throw new Exception("Test exception");
});
app.MapControllers();

app.Run();