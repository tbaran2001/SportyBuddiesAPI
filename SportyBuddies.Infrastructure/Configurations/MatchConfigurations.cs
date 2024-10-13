using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportyBuddies.Domain.MatchAggregate;
using SportyBuddies.Domain.MatchAggregate.ValueObjects;
using SportyBuddies.Domain.UserAggregate.ValueObjects;

namespace SportyBuddies.Infrastructure.Configurations;

public class MatchConfigurations : IEntityTypeConfiguration<Match>
{
    public void Configure(EntityTypeBuilder<Match> builder)
    {
        builder.HasKey(match => match.Id);

        builder.Property(match => match.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => MatchId.Create(value))
            .HasColumnOrder(0);


        builder.Property(match => match.UserId)
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value))
            .HasColumnOrder(1);

        builder.Property(match => match.MatchedUserId)
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value))
            .HasColumnOrder(2);
    }
}