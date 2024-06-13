using EndProject.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EndProject.Data.Configurations
{
    public class UserProfileMediaConfiguration : IEntityTypeConfiguration<UserProfileMedia>
    {
        public void Configure(EntityTypeBuilder<UserProfileMedia> builder)
        {
            builder.Property(x => x.ImageUrl).IsRequired();
            builder.HasOne(x => x.User)
                   .WithMany(x => x.UserProfileMedias)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
