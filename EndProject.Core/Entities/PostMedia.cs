namespace EndProject.Core.Entities;

public class PostMedia : BaseEntity
{
    public int PostId { get; set; }
    public string FilePath { get; set; } 
    public Post Post { get; set; }
}
