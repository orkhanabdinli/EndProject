using EndProject.Business.DTOs.PostDTOs;

namespace EndProject.Business.Services.Interfaces;

public interface IPostService
{
    Task CreatePostAsync(PostCreateDTO postCreateDTO);
    Task<List<PostGetDTO>> GetMyPostsAsync(string userId);
}
