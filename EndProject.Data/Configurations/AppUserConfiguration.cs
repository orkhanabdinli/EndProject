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
        builder.Property(x => x.PhoneNumber)
               .IsRequired()
               .HasMaxLength(10);
        builder.Property(x => x.Bio)
               .HasMaxLength(300);

        builder
            .HasMany(x => x.Comments)
            .WithOne(c => c.User)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
