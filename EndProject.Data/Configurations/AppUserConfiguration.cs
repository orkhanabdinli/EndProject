using EndProject.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EndProject.Data.Configurations;

public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.Property(x => x.FirstName)
               .IsRequired()
               .HasMaxLength(20);
        builder.Property(x => x.LastName)
               .IsRequired()
               .HasMaxLength(30);
        builder.Property(x => x.Email)
               .IsRequired()
               .HasMaxLength(50);

        builder
            .HasOne(x => x.UserAbout)
            .WithOne(x => x.User)
            .HasForeignKey<UserAbout>(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
