using EndProject.Core.Entities;
using EndProject.Data.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EndProject.Data.Contexts;

public class AppDbContext : IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    public DbSet<AppUser> Users {  get; set; }
    public DbSet<Post> Posts  {  get; set; }
    public DbSet<PostMedia> PostMedias  {  get; set; }
    public DbSet<FriendShip> FriendShips { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Like> Likes { get; set; }
    public DbSet<UserAbout> UsersAbout { get; set; }
    public DbSet<Conversation> Conversations { get; set; }
    public DbSet<Message> Messages { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppUserConfiguration).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
