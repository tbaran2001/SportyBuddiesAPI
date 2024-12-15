using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Profile.Domain.Enums;
using Profile.Domain.Models;

namespace Profile.Infrastructure.Configurations;

public class ProfileConfiguration : IEntityTypeConfiguration<Domain.Models.Profile>
{
    public void Configure(EntityTypeBuilder<Domain.Models.Profile> builder)
    {
        builder.HasKey(p => p.Id);

        builder.HasMany(p => p.ProfileSports)
            .WithOne()
            .HasForeignKey(p => p.ProfileId);

        builder.ComplexProperty(
            p => p.Name, nameBuilder =>
            {
                nameBuilder.Property(n => n.Value)
                    .HasColumnName(nameof(Domain.Models.Profile.Name));
            });

        builder.ComplexProperty(
            p => p.Description, descriptionBuilder =>
            {
                descriptionBuilder.Property(d => d.Value)
                    .HasColumnName(nameof(Domain.Models.Profile.Description));
            });

        builder.ComplexProperty(p => p.Preferences, preferencesBuilder =>
        {
            preferencesBuilder.Property(p => p.MinAge);
            preferencesBuilder.Property(p => p.MaxAge);
            preferencesBuilder.Property(p => p.MaxDistance);
            preferencesBuilder.Property(p => p.PreferredGender)
                .HasConversion(
                    gender => gender.ToString(),
                    dbGender => (Gender)Enum.Parse(typeof(Gender), dbGender));
        });
    }
}