using EndProject.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EndProject.Data.Configurations;

public class ConversationConfiguration : IEntityTypeConfiguration<Conversation>
{
    public void Configure(EntityTypeBuilder<Conversation> builder)
    {
        builder.Property(x => x.User1Id).IsRequired();
        builder.Property(x => x.User2Id).IsRequired();

        builder.HasOne(x => x.User1)
                .WithMany(x => x.ConversationUser1)
                .HasForeignKey(x => x.User1Id)
                .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.User2)
               .WithMany(x => x.ConversationUser2)
               .HasForeignKey(x => x.User2Id)
               .OnDelete(DeleteBehavior.NoAction);
    }
}
