using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportyBuddies.Domain.UserAggregate;
using SportyBuddies.Domain.UserAggregate.ValueObjects;

namespace SportyBuddies.Infrastructure.Configurations;

public class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        ConfigureUsers(builder);
        ConfigureUserSportIdsTable(builder);
    }

    private void ConfigureUsers(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.Id);

        builder.Property(user => user.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value));
    }

    private void ConfigureUserSportIdsTable(EntityTypeBuilder<User> builder)
    {
        builder.OwnsMany(u => u.SportIds, sportIds =>
        {
            sportIds.ToTable("UserSportIds");

            sportIds.HasKey("Id");

            sportIds.WithOwner().HasForeignKey("UserId");

            sportIds.Property(sportId => sportId.Value)
                .HasColumnName("SportId");
        });

        builder.Metadata.FindNavigation(nameof(User.SportIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}