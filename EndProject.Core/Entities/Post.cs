namespace EndProject.Core.Entities;

public class Post : BaseEntity
{
    public string UserId { get; set; }
    public string Description { get; set; }
    public AppUser User { get; set; }
}
