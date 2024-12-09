using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportyBuddies.Domain.Matches;

namespace SportyBuddies.Infrastructure.Configuration;

public class MatchConfiguration: IEntityTypeConfiguration<Match>
{
    public void Configure(EntityTypeBuilder<Match> builder)
    {
        builder
            .HasOne(m => m.User)
            .WithMany()
            .HasForeignKey(m => m.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(m => m.MatchedUser)
            .WithMany()
            .HasForeignKey(m => m.MatchedUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}