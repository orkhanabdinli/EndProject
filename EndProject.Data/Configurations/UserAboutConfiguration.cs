using EndProject.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EndProject.Data.Configurations;

public class UserAboutConfiguration : IEntityTypeConfiguration<UserAbout>
{
    public void Configure(EntityTypeBuilder<UserAbout> builder)
    {
        builder.Property(x => x.Bio)
               .HasMaxLength(300);
        builder.Property(x => x.Country)
               .HasMaxLength(100);
        builder.Property(x => x.Gender)
               .IsRequired()
               .HasMaxLength(50);
    }
}
