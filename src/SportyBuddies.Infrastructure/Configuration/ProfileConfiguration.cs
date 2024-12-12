using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportyBuddies.Domain.Profiles;
using SportyBuddies.Domain.Sports;

namespace SportyBuddies.Infrastructure.Configuration;

public class ProfileConfiguration:IEntityTypeConfiguration<Profile>
{
    public void Configure(EntityTypeBuilder<Profile> builder)
    {
        builder
            .HasMany(u => u.Sports)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "ProfileSports",
                j => j
                    .HasOne<Sport>()
                    .WithMany()
                    .HasForeignKey("SportId"),
                j => j
                    .HasOne<Profile>()
                    .WithMany()
                    .HasForeignKey("ProfileId")
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