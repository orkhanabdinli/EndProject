namespace EndProject.MVC.ViewModels;

public class PostViewModel
{
    public string UserId { get; set; }
    public int PostId { get; set; }
    public int LikesCount { get; set; }
    public string UserName { get; set; }
    public string Description { get; set; }
    public string UserProfileImageUrl { get; set; }
    public string ElapsedTime { get; set;}
    public string UploadTime { get; set;}
    public List<PostMediaViewModel> PostMedias { get; set; }
}
