using EndProject.Business.DTOs.PostMediaDTOs;
using EndProject.Core.Entities;

namespace EndProject.Business.DTOs.PostDTOs;

public class PostGetDTO
{
    public string UserId { get; set; }
    public int PostId { get; set; }
    public int LikesCount { get; set; }
    public string UserName { get; set; }
    public string? Description { get; set; }
    public string UserProfileImageUrl { get; set; }
    public string ElapsedTime { get; set; }
    public string UploadTime { get; set; }
    public List<PostMediaGetDTO>? PostMedias { get; set; }
}
