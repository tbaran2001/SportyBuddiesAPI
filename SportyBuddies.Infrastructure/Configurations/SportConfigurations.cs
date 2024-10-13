using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportyBuddies.Domain.SportAggregate;
using SportyBuddies.Domain.SportAggregate.ValueObjects;

namespace SportyBuddies.Infrastructure.Configurations;

public class SportConfigurations : IEntityTypeConfiguration<Sport>
{
    public void Configure(EntityTypeBuilder<Sport> builder)
    {
        builder.HasKey(sport => sport.Id);

        builder.Property(sport => sport.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => SportId.Create(value));

        // Configure other properties as needed
    }
}