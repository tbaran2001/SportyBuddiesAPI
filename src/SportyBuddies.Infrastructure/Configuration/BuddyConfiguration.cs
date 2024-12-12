using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportyBuddies.Domain.Buddies;

namespace SportyBuddies.Infrastructure.Configuration;

public class BuddyConfiguration: IEntityTypeConfiguration<Buddy>
{
    public void Configure(EntityTypeBuilder<Buddy> builder)
    {
        builder.HasOne(b => b.Profile)
            .WithMany()
            .HasForeignKey(b => b.ProfileId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(b => b.MatchedProfile)
            .WithMany()
            .HasForeignKey(b => b.MatchedProfileId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}