using EndProject.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EndProject.Data.Configurations;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.Property(x => x.Text)
               .IsRequired()
               .HasMaxLength(500);
        builder.HasOne(x => x.Post)
               .WithMany(x => x.Comments)
               .HasForeignKey(c => c.PostId)
               .OnDelete(DeleteBehavior.NoAction);
    }
}
