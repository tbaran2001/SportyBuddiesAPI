using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SportyBuddies.Domain.Constants;
using SportyBuddies.Domain.Sports;
using SportyBuddies.Infrastructure;

namespace SportyBuddies.Api.Extensions;

public static class SeedDataExtensions
{
    public static void SeedData(this IApplicationBuilder app)
    {
        var scope = app.ApplicationServices.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<SportyBuddiesDbContext>();

        if (dbContext.Database.GetPendingMigrations().Any())
            dbContext.Database.Migrate();

        if (!dbContext.Database.CanConnect())
            return;

        if (!dbContext.Sports.Any())
            SeedSports(dbContext);
        if (!dbContext.Roles.Any())
            SeedRoles(dbContext);
    }

    private static void SeedRoles(SportyBuddiesDbContext dbContext)
    {
        var roles = new List<IdentityRole<Guid>>
        {
            new(UserRoles.User)
            {
                NormalizedName = UserRoles.User.ToUpper()
            },
            new(UserRoles.Owner)
            {
                NormalizedName = UserRoles.Owner.ToUpper()
            },
            new(UserRoles.Admin)
            {
                NormalizedName = UserRoles.Admin.ToUpper()
            },
        };


        dbContext.Roles.AddRange(roles);
        dbContext.SaveChanges();
    }

    private static void SeedSports(SportyBuddiesDbContext dbContext)
    {
        var faker = new Faker();

        var sports = new List<Sport>
        {
            Sport.Create("Football", faker.Lorem.Sentence()),
            Sport.Create("Basketball", faker.Lorem.Sentence()),
            Sport.Create("Volleyball", faker.Lorem.Sentence()),
            Sport.Create("Tennis", faker.Lorem.Sentence()),
            Sport.Create("Table Tennis", faker.Lorem.Sentence()),
            Sport.Create("Badminton", faker.Lorem.Sentence()),
            Sport.Create("Swimming", faker.Lorem.Sentence()),
            Sport.Create("Running", faker.Lorem.Sentence()),
            Sport.Create("Cycling", faker.Lorem.Sentence()),
            Sport.Create("Gym", faker.Lorem.Sentence()),
            Sport.Create("Yoga", faker.Lorem.Sentence()),
            Sport.Create("Pilates", faker.Lorem.Sentence()),
            Sport.Create("Dance", faker.Lorem.Sentence()),
            Sport.Create("Boxing", faker.Lorem.Sentence()),
            Sport.Create("Martial Arts", faker.Lorem.Sentence()),
            Sport.Create("Hiking", faker.Lorem.Sentence()),
            Sport.Create("Climbing", faker.Lorem.Sentence()),
            Sport.Create("Skiing", faker.Lorem.Sentence()),
            Sport.Create("Snowboarding", faker.Lorem.Sentence()),
            Sport.Create("Surfing", faker.Lorem.Sentence()),
        };

        dbContext.Sports.AddRange(sports);
        dbContext.SaveChanges();
    }
}