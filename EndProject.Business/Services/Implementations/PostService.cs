using EndProject.Business.DTOs.PostDTOs;
using EndProject.Business.DTOs.PostMediaDTOs;
using EndProject.Business.Services.Interfaces;
using EndProject.Business.Utilities.CustomExceptions.NotFoundExceptions;
using EndProject.Business.Utilities.Extensions;
using EndProject.Core.Entities;
using EndProject.Core.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;

namespace EndProject.Business.Services.Implementations;

public class PostService : IPostService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IPostRepository _postRepository;
    private readonly IPostMediaRepository _postMediaRepository;
    private readonly IUserProfileMediaRepository _userProfileMediaRepository;
    private readonly IWebHostEnvironment _env;
    private readonly string _rootPath;
    private readonly string _folderName = "PostMedias";
    public PostService(IPostRepository postRepository,
                       IPostMediaRepository postMediaRepository,
                       IUserProfileMediaRepository userProfileMediaRepository,
                       IWebHostEnvironment env,
                       UserManager<AppUser> userManager)
    {
        _userManager = userManager;
        _postRepository = postRepository;
        _postMediaRepository = postMediaRepository;
        _userProfileMediaRepository = userProfileMediaRepository;
        _env = env;
        _rootPath = $"{_env.WebRootPath}";
    }
    public async Task CreatePostAsync(PostCreateDTO postCreateDTO)
    {
        var user = await _userManager.FindByIdAsync(postCreateDTO.UserId);
        if (user is null) throw new UserNotFoundException(404, "User not found");
        Post post = new Post()
        {
            UserId = user.Id,
            Description = postCreateDTO.Description,
            IsActive = true,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
        };
        await _postRepository.InsertAsync(post);
        await _postRepository.CommitAsync();
        if (postCreateDTO.PostMedias is not null)
        {
            foreach (var media in postCreateDTO.PostMedias)
            {
                var filePath = media.SaveFile(_rootPath, _folderName);
                PostMedia postMedia = new PostMedia()
                {
                    PostId = post.Id,
                    FilePath = filePath,
                    IsActive = true
                };
                await _postMediaRepository.InsertAsync(postMedia);
            }
            await _postMediaRepository.CommitAsync();
        }
    }

    public async Task<List<PostGetDTO>> GetMyPostsAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null) throw new UserNotFoundException(404, "User not found");
        var MyPosts = await _postRepository.GetAllAsync(x => x.UserId == user.Id && x.IsActive == true, "PostMedias");
        var ProfileImage = await _userProfileMediaRepository.GetSingleAsync(x => x.UserId == userId && x.IsBackgroundImage == false);
        List<PostGetDTO> posts = new List<PostGetDTO>();
        if (MyPosts.Count > 0)
        {
            foreach (var MyPost in MyPosts)
            {
                PostGetDTO Post = new PostGetDTO()
                {
                    UserId = userId,
                    PostId = MyPost.Id,
                    UserName = user.UserName,
                    Description = MyPost.Description,
                    LikesCount = MyPost.LikesCount,
                    UserProfileImageUrl = ProfileImage.ImageUrl,
                    PostMedias = MyPost.PostMedias?.Select(x => new PostMediaGetDTO
                    {
                        UserId = userId,
                        PostId = MyPost.Id,
                        MediaUrl = x.FilePath

                    }).ToList(),
                    ElapsedTime = MyPost.CreatedDate.GetElapsedTime(),
                    UploadTime = MyPost.CreatedDate.ToString("MMMM d, yyyy"),
                    //Comments = UserPost.Comments.Select(c => new CommentGetDto
                    //{
                    //    UserId = c.UserId,
                    //    CommentId = c.Id,
                    //    Text = c.Text,
                    //    UserName = c.User.UserName,
                    //    UserProfileImage = c.User.Images.FirstOrDefault(i => i.IsPostImage == false).ImageUrl,
                    //    ElapsedTime = c.CreatedDate.GetElapsedTime(),
                    //}).ToList()
                };
                posts.Add(Post);
            }
            return posts;
        }
        return posts;
    }
}
