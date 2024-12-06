using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportyBuddies.Domain.Buddies;

namespace SportyBuddies.Infrastructure.Configuration;

public class BuddyConfiguration: IEntityTypeConfiguration<Buddy>
{
    public void Configure(EntityTypeBuilder<Buddy> builder)
    {
        builder.HasOne(b => b.User)
            .WithMany()
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(b => b.MatchedUser)
            .WithMany()
            .HasForeignKey(b => b.MatchedUserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}