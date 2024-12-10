using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportyBuddies.Domain.Sports;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Infrastructure.Configuration;

public class UserConfiguration:IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasMany(u => u.Sports)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "UserSports",
                j => j
                    .HasOne<Sport>()
                    .WithMany()
                    .HasForeignKey("SportId"),
                j => j
                    .HasOne<User>()
                    .WithMany()
                    .HasForeignKey("UserId")
            );

        builder
            .Property(up => up.Id)
            .ValueGeneratedNever();

        builder
            .OwnsOne(u => u.Preferences);

        builder
            .HasMany(u=>u.Messages)
            .WithOne(m=>m.Sender)
            .HasForeignKey(m=>m.SenderId);

        builder
            .HasMany(u=>u.Conversations)
            .WithOne(c=>c.Creator)
            .HasForeignKey(c=>c.CreatorId);
    }
}