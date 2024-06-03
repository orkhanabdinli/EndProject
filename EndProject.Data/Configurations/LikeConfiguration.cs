using EndProject.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EndProject.Data.Configurations;

public class LikeConfiguration : IEntityTypeConfiguration<Like>
{
    public void Configure(EntityTypeBuilder<Like> builder)
    {
        builder.HasOne(x => x.Post)
               .WithMany(x => x.Likes)
               .HasForeignKey(c => c.PostId)
               .OnDelete(DeleteBehavior.NoAction);
    }
}
