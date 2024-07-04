namespace EndProject.Core.Entities;

public class Post : BaseEntity
{
    public string UserId { get; set; }
    public string Description { get; set; }
    public int LikesCount { get; set; }
    public AppUser User { get; set; }
    public List<Comment>? Comments { get; set; }
    public List<Like>? Likes { get; set; }
    public List<PostMedia>? PostMedias { get; set; }
}
